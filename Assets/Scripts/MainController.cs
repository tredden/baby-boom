using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct SongConfig {
    public AudioClip audioClip;
    public double bpm;
    public int columns;
    public int rows;
    public int spawnBabyPeriod;
}

public class MainController : MonoBehaviour
{
    private AudioSource track;
    public double bpm;
    public GameObject baby; // The baby template
    public List<GameObject> spawnPoints;
    public List<GameObject> launcherPoints;

    public List<GameObject> bagPoints;

    private double lastTimeToNextBeat;
    private double timeToNextBeat;
    private double beat;
    private int currBeat;
    private GameObject songBeatText;
    private double songLengthBeats;
    private int rows = 3;
    private int columns = 1;
    private int spawnBabyPeriod = 2;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        track = GetComponent<AudioSource>();
        // Get the global variable holder game object
        GameObject globalVariableHolder = GameObject.Find("GlobalVariableHolder");
        // Get the song config from the global variable holder
        SongConfig song = globalVariableHolder.GetComponent<GlobalVariableHolder>().song;
        // Configure the game
        configure(song);

        lastTimeToNextBeat = 0;
        currBeat = -1;
        //songBeatText = GameObject.Find("SongBeatText");
        songLengthBeats = ToBeat(track.clip.samples);

        track.Play();
        yield return new WaitForSeconds(track.clip.length + 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Results");
    }

    void Update()
    {
        int currentSample = track.timeSamples;
        beat = ToBeat(currentSample);

        timeToNextBeat = Mathf.Ceil((float)(beat)) - beat;
        //songBeatText.GetComponent<TextMeshProUGUI>().SetText("Beat: " + beat.ToString("F2"));

        if (lastTimeToNextBeat < timeToNextBeat)
        {
            currBeat++;
            if (currBeat % spawnBabyPeriod == 0 && beat + 5 < songLengthBeats)
            {
                Spawnbaby(Mathf.CeilToInt(Mathf.Pow(Random.Range(0.0f,1.0f),2)*3));
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
        int bag = row * 3 + col;
        GameObject newbaby = Instantiate(baby, spawnPoints[col]
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

    public void configure(SongConfig config)
    {
        track.clip = config.audioClip;
        bpm = config.bpm;
        columns = config.columns;
        rows = config.rows;
        spawnBabyPeriod = config.spawnBabyPeriod;
    }
}
