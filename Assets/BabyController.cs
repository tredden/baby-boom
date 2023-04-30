using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour
{
    
    private GameObject main;
    private Vector2 startPos;
    private Vector2 launchPos;
    public int bag;
    private int beatsToFly;
    private int currBeat;
    private float lastTimeToNextBeat;
    private float startBeat;
    private float endBeat;

    Dictionary<int, string> bagToKey = new Dictionary<int, string>();
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("MainController");
        startPos = transform.position;
        launchPos = main.GetComponent<MainController>().launcherPoints[bag%3].transform.position;
        beatsToFly = 4;
        currBeat = -1;
        lastTimeToNextBeat = 0;
        
        
        startBeat = Mathf.FloorToInt((float)main.GetComponent<MainController>().GetBeat());
        endBeat = startBeat + beatsToFly;
        
        
        bagToKey.Add(0,"a");
        bagToKey.Add(1,"s");
        bagToKey.Add(2,"d");

        //Debug.Log(startPos + " " + launchPos + " | " + startBeat + " " + endBeat);
    }

    // Update is called once per frame
    void Update()
    {
        float timeToNextBeat = (float)main.GetComponent<MainController>().GetNextBeat();
        Color newColor = transform.GetComponent<SpriteRenderer>().color;
        newColor.a = Mathf.Pow(timeToNextBeat, 2);
        transform.GetComponent<SpriteRenderer>().color = newColor;
        
        if (lastTimeToNextBeat < timeToNextBeat) {
            //Debug.Log(currBeat);
            currBeat++;
        }
        lastTimeToNextBeat = timeToNextBeat;

        if (currBeat > beatsToFly){
            Destroy(gameObject);
        }
        //Debug.Log(startPos + " " + launchPos);
        transform.position = Vector2.Lerp(startPos, launchPos, (float)currBeat/beatsToFly);

        float acc; // in beats
        acc = Mathf.Abs((float)main.GetComponent<MainController>().GetBeat() - endBeat);
        //Debug.Log(acc);
        if (Input.GetKeyDown(bagToKey[bag])){
            if (acc < 0.2){
                Debug.Log("Win");
            }else{
                Debug.Log("Lose");
            }
        } 
    }
}
