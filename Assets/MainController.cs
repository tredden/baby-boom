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
            Debug.Log(currBeat);
            currBeat++;
            if (currBeat%4 == 0){
                Spawnbaby();
            }
        }
        lastTimeToNextBeat = timeToNextBeat;
        
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
