using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private AudioSource track;
    public double bpm;
    public GameObject baby;
    private double lastTimeToNextBeat;
    public List<GameObject> spawnPoints;
    public List<GameObject> launcherPoints;
    
    public List<GameObject> bagPoints;

    private double timeToNextBeat;
    private double beats;
    private int currBeat;
    // Start is called before the first frame update
    void Start()
    {
        track = GetComponent<AudioSource>();
        lastTimeToNextBeat = 0;
        currBeat = -1;
    }

    // Update is called once per frame
    void Update()
    {
        int currentSample = track.timeSamples;
        double sampleRate = track.clip.frequency;
        double timeInSeconds = ((double)currentSample) / sampleRate;
        beats = timeInSeconds * bpm / 60.0;
        timeToNextBeat = Mathf.Ceil((float)(beats)) - beats;

        if (lastTimeToNextBeat < timeToNextBeat) {
            currBeat++;
            if (currBeat%4 == 0){
                Spawnbaby();
            }
        }
        lastTimeToNextBeat = timeToNextBeat;
        


        Color col;
        col = launcherPoints[0].GetComponent<SpriteRenderer>().color;
        //Debug.Log(Input.GetKeyDown("a"));
        if (Input.GetKeyDown("a")){
            col.r = 0.8f;
        } 
        
        if (Input.GetKeyUp("a")){
            col.r = 0.2f;
        }
        launcherPoints[0].GetComponent<SpriteRenderer>().color = col;
        
        col = launcherPoints[1].GetComponent<SpriteRenderer>().color;
        if (Input.GetKeyDown("s")){
            col.r = 0.8f;
        }
        if (Input.GetKeyUp("s")){
            col.r = 0.2f;
        }
        launcherPoints[1].GetComponent<SpriteRenderer>().color = col;
        
        col = launcherPoints[2].GetComponent<SpriteRenderer>().color;
        if (Input.GetKeyDown("d")){
            col.r = 0.8f;
        }
        if (Input.GetKeyUp("d")){
            col.r = 0.2f;
        }
        launcherPoints[2].GetComponent<SpriteRenderer>().color = col;
    }

    void Spawnbaby(){
        int bag = Random.Range(0,3);// 9 total
        GameObject newbaby = Instantiate(baby, spawnPoints[bag%3].transform.position, Quaternion.identity);
        newbaby.GetComponent<BabyController>().bag = bag;
        //Instantiate(baby, midSpawnPoint.transform);
        //Instantiate(baby, rightSpawnPoint.transform);
        
    }
    
    public double GetNextBeat(){
        return timeToNextBeat;
    }
    public double GetBeat(){
        return beats;
    }
}
