using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour
{
    
    private GameObject main;
    private Vector2 startPos;
    private Vector2 launchPos;
    public int bag;
    private int beatsToLaunch;
    private int beatsToFly;
    private int currBeat;
    private float lastTimeToNextBeat;
    private float startBeat;
    private float endBeat;
    private bool isFlying;
    private GameObject target;
    Dictionary<int, string> bagToKey = new Dictionary<int, string>();
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("MainController");
        startPos = transform.position;
        launchPos = main.GetComponent<MainController>().launcherPoints[bag%3].transform.position;
        beatsToLaunch = 4;
        beatsToFly = 2;
        currBeat = -1;
        lastTimeToNextBeat = 0;
        target = main.GetComponent<MainController>().bagPoints[bag];
        
        startBeat = Mathf.FloorToInt((float)main.GetComponent<MainController>().GetBeat());
        endBeat = startBeat + beatsToLaunch;
        
        
        bagToKey.Add(0,"a");
        bagToKey.Add(1,"s");
        bagToKey.Add(2,"d");

        isFlying = false;
        //Debug.Log(startPos + " " + launchPos + " | " + startBeat + " " + endBeat);
    }

    // Update is called once per frame
    void Update()
    {
        float timeToNextBeat = (float)main.GetComponent<MainController>().GetNextBeat();
        Color newColor = transform.GetComponent<SpriteRenderer>().color;
        newColor.a = Mathf.Pow(timeToNextBeat, 2) / 2 + 0.5f;
        transform.GetComponent<SpriteRenderer>().color = newColor;
        
        if (lastTimeToNextBeat < timeToNextBeat) {
            //Debug.Log(currBeat);
            currBeat++;
        }
        lastTimeToNextBeat = timeToNextBeat;

        if(isFlying){
            transform.position = Vector2.Lerp(launchPos, target.transform.position, Mathf.Sqrt(((float)main.GetComponent<MainController>().GetBeat()-endBeat)/beatsToFly));
            if (currBeat > beatsToFly){
                Destroy(gameObject);
            }
        } else {
            if (currBeat > beatsToLaunch){
                Destroy(gameObject);
            }
            //Debug.Log(startPos + " " + launchPos);
            transform.position = Vector2.Lerp(startPos, launchPos, (float)currBeat/beatsToLaunch);

            float acc; // in beats
            acc = Mathf.Abs((float)main.GetComponent<MainController>().GetBeat() - endBeat);
            //Debug.Log(acc);
            if (Input.GetKeyDown(bagToKey[bag])){
                if (acc < 0.2){
                    isFlying = true;
                    currBeat = 0;
                }else{
                    Debug.Log("Lose");
                }
            }
        }
    }

    
}
