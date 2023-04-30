using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private AudioSource track;
    public double bpm;
    public GameObject baby;
    private double lastTimeToNextBeat;
    // Start is called before the first frame update
    void Start()
    {
        track = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentSample = track.timeSamples;
        double sampleRate = track.clip.frequency;
        double timeInSeconds = ((double)currentSample) / sampleRate;
        double beats = timeInSeconds * bpm / 60.0;
        double timeToNextBeat = Mathf.Ceil((float)(beats)) - beats;

        Color newColor = baby.GetComponent<SpriteRenderer>().color;
        newColor.a = Mathf.Pow((float)timeToNextBeat, 3);
        baby.GetComponent<SpriteRenderer>().color = newColor;

        
        if (lastTimeToNextBeat < timeToNextBeat) {
            Debug.Log("Beat");
        }
        lastTimeToNextBeat = timeToNextBeat;
    }
}
