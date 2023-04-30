using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private double beat;
    private int currBeat;
    private GameObject songBeatText;
    private double songLengthBeats;
    public int rows = 1;

    // Start is called before the first frame update
    void Start()
    {
        track = GetComponent<AudioSource>();
        lastTimeToNextBeat = 0;
        currBeat = -1;
        songBeatText = GameObject.Find("SongBeatText");
        songLengthBeats = ToBeat(track.clip.samples);
    }

    // Update is called once per frame
    void Update()
    {
        int currentSample = track.timeSamples;
        beat = ToBeat(currentSample);

        timeToNextBeat = Mathf.Ceil((float)(beat)) - beat;
        songBeatText.GetComponent<TextMeshProUGUI>().SetText("Beat: " + beat.ToString("F2"));


        if (lastTimeToNextBeat < timeToNextBeat)
        {
            currBeat++;
            if (currBeat % 1 == 0 && beat + 5 < songLengthBeats)
            {
                Spawnbaby(Random.Range(0, 3) + 1);
            }
        }
        lastTimeToNextBeat = timeToNextBeat;

        Color col;
        col = launcherPoints[0].GetComponent<SpriteRenderer>().color;

        if (Input.GetKeyDown("a"))
        {
            col.r = 0.8f;
        }

        if (Input.GetKeyUp("a"))
        {
            col.r = 0.2f;
        }
        launcherPoints[0].GetComponent<SpriteRenderer>().color = col;

        col = launcherPoints[1].GetComponent<SpriteRenderer>().color;
        if (Input.GetKeyDown("s"))
        {
            col.r = 0.8f;
        }
        if (Input.GetKeyUp("s"))
        {
            col.r = 0.2f;
        }
        launcherPoints[1].GetComponent<SpriteRenderer>().color = col;

        col = launcherPoints[2].GetComponent<SpriteRenderer>().color;
        if (Input.GetKeyDown("d"))
        {
            col.r = 0.8f;
        }
        if (Input.GetKeyUp("d"))
        {
            col.r = 0.2f;
        }
        launcherPoints[2].GetComponent<SpriteRenderer>().color = col;
    }

    private void SpawnInCol(int col)
    {
        int row = Random.Range(0, rows);
        int bag = (row * 3 + col) % 3;
        GameObject newbaby = Instantiate(baby, spawnPoints[bag % 3]
                                         .transform.position, Quaternion.identity);
        newbaby.GetComponent<BabyController>().bag = bag;
    }

    void Spawnbaby(int count)
    {
        if (count == 3)
        {
            SpawnInCol(0);
            SpawnInCol(1);
            SpawnInCol(2);
        }
        else if (count == 2)
        {
            int skipCol = Random.Range(0, 3);
            SpawnInCol((skipCol + 1) % 3);
            SpawnInCol((skipCol + 2) % 3);
        } else
        {
            SpawnInCol(Random.Range(0, 3));
        }
    }

    public double GetNextBeat()
    {
        return timeToNextBeat;
    }
    public double GetBeat()
    {
        return beat;
    }
    private double ToBeat(int sampleIndex)
    {
        double sampleRate = track.clip.frequency;
        double timeInSeconds = ((double)sampleIndex) / sampleRate;
        return timeInSeconds * bpm / 60.0;
    }
}
