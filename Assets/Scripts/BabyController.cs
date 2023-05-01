using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour
{
    private GameObject main;
    private Vector2 startPos;
    private Vector2 launchPos;
    public int bag;
    public int beatsUntilLaunch;
    private float startBeat;
    private float endBeat;
    private bool isFlying;
    private GameObject target;
    private double launchBeat;
    public List<GameObject> words;
    private float accuracy; // in beats

    Dictionary<int, string> bagToKey = new Dictionary<int, string>();
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("MainController");
        MainController mainController = main.GetComponent<MainController>();
        startPos = transform.position;
        launchPos = mainController.launcherPoints[bag % 3].transform.position;

        target = mainController.bagPoints[bag];
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = 
            target.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color;
        startBeat = Mathf.FloorToInt((float)main.GetComponent<MainController>().GetBeat());
        endBeat = startBeat + beatsUntilLaunch;

        bagToKey.Add(0, "a");
        bagToKey.Add(1, "s");
        bagToKey.Add(2, "d");

        bagToKey.Add(3, "q");
        bagToKey.Add(4, "w");
        bagToKey.Add(5, "e");

        bagToKey.Add(6, "z");
        bagToKey.Add(7, "x");
        bagToKey.Add(8, "c");

        isFlying = false;
        //Debug.Log(startPos + " " + launchPos + " | " + startBeat + " " + endBeat);
    }

    // Update is called once per frame
    void Update()
    {
        double beat = main.GetComponent<MainController>().GetBeat() - startBeat;

        int beatInt = Mathf.FloorToInt((float)beat);

        // Pulse the alpha with the beat.
        Color newColor = transform.GetComponent<SpriteRenderer>().color;
        newColor.a = 1 - Mathf.Pow((float)(beat % 1), 2) / 2;
        transform.GetComponent<SpriteRenderer>().color = newColor;

        if (isFlying)
        {
            // 0 at launchBeat
            // 1 at inBagBeat
            int inBagBeat = beatsUntilLaunch + 1;
            float flyProgress = (float)((beat - launchBeat) / (inBagBeat - launchBeat));
            if (float.IsNaN(flyProgress)) {
                Debug.Log("It's nan" + beat + " " + launchBeat + " " + inBagBeat);
            }
                    
            transform.position = Vector2.Lerp(
                launchPos,
                target.transform.position,
                Mathf.Sqrt(flyProgress));

            if (flyProgress > 1)
            {
                Destroy(gameObject);
                target.transform.GetChild(0).GetComponent<Animator>().SetTrigger("catchBaby");
                if (accuracy < 0.2){
                    PlayWord(2);
                    main.GetComponent<MainController>().IncScore(1000);
                } else {
                    PlayWord(1);
                    main.GetComponent<MainController>().IncScore(300);
                }
            }
        }
        else
        {
            if (beat > beatsUntilLaunch + 1)
            {
                Destroy(gameObject);
                PlayWord(0);
            }
            float progress = (float)beatInt / beatsUntilLaunch;
            transform.position = Vector2.Lerp(startPos, launchPos, progress);

            
            accuracy = Mathf.Abs((float)main.GetComponent<MainController>().GetBeat() - endBeat);
            //Debug.Log(acc);
            if (Input.GetKeyDown(bagToKey[bag]))
            {
                if (accuracy < 0.5)
                {
                    launchBeat = beat;
                    Debug.Log("Launch " + launchBeat);
                    isFlying = true;
                }
                else
                {
                    Debug.Log("Lose");
                }
            }
        }
    }

    void PlayWord(int word){
        GameObject newword = Instantiate(words[word], target.transform);
        newword.GetComponent<WordEffect>().word = word;
    }
}
