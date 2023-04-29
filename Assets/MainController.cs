using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour

{
    private AudioSource track;
    public double bpm;
    private double timeToBeat;
    private int prev;
    public GameObject baby;
    // Start is called before the first frame update
    void Start()
    {
        track = GetComponent<AudioSource>();
        double timeToBeat = 0;
        int prev = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int timeStamp = track.timeSamples; // timestamp in samples
        int trackLength = track.clip.samples; // length in samples
        double freq = track.clip.frequency; // frequency of audioclip
        double samplesPerBeat = freq * 60 / bpm;

        
        int elapsed = timeStamp - prev;
        if(elapsed<0){
            elapsed += trackLength;
        }
        prev = timeStamp;
        
        Color newColor = baby.GetComponent<SpriteRenderer>().color;
        newColor.a = Mathf.Pow((float)(timeToBeat / samplesPerBeat),3);
        baby.GetComponent<SpriteRenderer>().color = newColor;

        timeToBeat -= elapsed;
        Debug.Log(timeToBeat);
        if (timeToBeat <= 0){
            Debug.Log("Beat");
            timeToBeat += samplesPerBeat;
            //baby.SetActive(true);
        } else {
            //baby.SetActive(false);
        }

    }
}
