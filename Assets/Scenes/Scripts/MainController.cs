using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct SongConfig
{
    public int prngSeed;
    public AudioClip audioClip;
    public double bpm;
    public int columns;
    public int rows;
    public int spawnBabyPeriod;
    public double oneBabyDensity;
    public double twoBabyDensity;
    public double threeBabyDensity;
    public int beatsPerMeasure;
    public int babySpawnCutoff;
}

public class MainController : MonoBehaviour
{
    private AudioSource track;
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
    private GameObject globalVariableHolder;
    //public GameObject accessible;
    private int spawnUntil;
    private SongConfig song;
    private int[] babiesEachBeat;

    // Start is called before the first frame update
    void Start()
    {
        track = GetComponent<AudioSource>();
        // Get the global variable holder game object
        globalVariableHolder = GameObject.Find("GlobalVariableHolder");
        if (globalVariableHolder != null)
        {
            song = globalVariableHolder.GetComponent<GlobalVariableHolder>().song;
            track.clip = song.audioClip;
        }

        if(song.columns<3){
            GameObject.Find("LeftConveyor").SetActive(false);
            bagPoints[0].SetActive(false);
            bagPoints[3].SetActive(false);
            bagPoints[6].SetActive(false);
            if(song.columns<2){
                GameObject.Find("RightConveyor").SetActive(false);
                bagPoints[2].SetActive(false);
                bagPoints[5].SetActive(false);
                bagPoints[8].SetActive(false);
            }
        }

        if(song.rows<3){
            bagPoints[6].SetActive(false);
            bagPoints[7].SetActive(false);
            bagPoints[8].SetActive(false);
            if(song.rows<2){
                bagPoints[3].SetActive(false);
                bagPoints[4].SetActive(false);
                bagPoints[5].SetActive(false);
            }
        }
        lastTimeToNextBeat = 0;
        currBeat = -1;
        songBeatText = GameObject.Find("Score");
        songLengthBeats = ToBeat(track.clip.samples);

        // Only spawn babies if they will reach the end in time.
        int timeToReachEnd = song.babySpawnCutoff;
        spawnUntil = Mathf.FloorToInt((float)songLengthBeats) - timeToReachEnd;
        // Create a PRNG with the seed from the song config
        System.Random prng = new System.Random(song.prngSeed);

        // Initialize an array of the number of babies to spawn on each beat
        babiesEachBeat = new int[System.Math.Max(
                Mathf.CeilToInt(spawnUntil / song.spawnBabyPeriod), 0) + 1];

        int onesEnd = Mathf.FloorToInt((float)(babiesEachBeat.Length * song.oneBabyDensity));
        System.Array.Fill(babiesEachBeat, 1, 0, onesEnd);

        int twosCount = Mathf.FloorToInt((float)(babiesEachBeat.Length * song.twoBabyDensity));
        System.Array.Fill(babiesEachBeat, 2, onesEnd, twosCount);

        int twosEnd = twosCount + onesEnd;
        int threesCount = Mathf.FloorToInt((float)(babiesEachBeat.Length * song.threeBabyDensity));
        System.Array.Fill(babiesEachBeat, 3, twosEnd, threesCount);

        shuffle(babiesEachBeat, prng);
        Debug.Log("Pattern: " + string.Join(" ", babiesEachBeat));

        track.Play();
    }

    void shuffle<T>(T[] array, System.Random prng)
    {
        // Fisher-Yates
        for (int i = 1; i < array.Length; i++)
        {
            int index = prng.Next(0, array.Length - i);
            int dest = array.Length - i;

            T tmp = array[dest];
            array[dest] = array[index];
            array[index] = tmp;
        }
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
            if (currBeat % song.spawnBabyPeriod == 0 && beat < spawnUntil)
            {
                int count = babiesEachBeat[currBeat / song.spawnBabyPeriod];
                SpawnBabies(count);
                //SpawnBabies(Mathf.CeilToInt(Mathf.Pow(Random.Range(0.0f,1.0f),2)*3));
            }

            GameObject.Find("MidConveyor").transform.GetChild(2).GetComponent<Animator>().SetTrigger("Convey");
            if(song.columns > 1){
                GameObject.Find("RightConveyor").transform.GetChild(2).GetComponent<Animator>().SetTrigger("Convey");
                if(song.columns > 2){
                    GameObject.Find("LeftConveyor").transform.GetChild(2).GetComponent<Animator>().SetTrigger("Convey");
                }
            }
        }
        lastTimeToNextBeat = timeToNextBeat;

        if (currBeat > songLengthBeats - 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Results");
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            UnityEngine.SceneManagement.SceneManager.LoadScene("Songs");
        }

        if (globalVariableHolder.GetComponent<GlobalVariableHolder>().showLetters){
            foreach(GameObject letter in GameObject.FindGameObjectsWithTag("KeyOverlay")){
                letter.GetComponent<SpriteRenderer>().enabled=true;
            }
        } else {
            foreach(GameObject letter in GameObject.FindGameObjectsWithTag("KeyOverlay")){
                letter.GetComponent<SpriteRenderer>().enabled=false;
            }
        }

        animateLauncher();
    }

    private void animateLauncher()
    {
        // Color col;
        // col = launcherPoints[0].GetComponent<SpriteRenderer>().color;

        if (Input.GetKeyDown("a") || Input.GetKeyDown("q") || Input.GetKeyDown("z"))
        {
            //col.r = 0.8f;
            launcherPoints[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Bounce");
        }

        // if (Input.GetKeyUp("a"))
        // {
        //     col.r = 0.2f;
        // }
        // launcherPoints[0].GetComponent<SpriteRenderer>().color = col;

        //col = launcherPoints[1].GetComponent<SpriteRenderer>().color;
        if (Input.GetKeyDown("s") || Input.GetKeyDown("w") || Input.GetKeyDown("x"))
        {
            // col.r = 0.8f;
            launcherPoints[1].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Bounce");
        }
        // if (Input.GetKeyUp("s"))
        // {
        //     col.r = 0.2f;
        // }
        // launcherPoints[1].GetComponent<SpriteRenderer>().color = col;

        // col = launcherPoints[2].GetComponent<SpriteRenderer>().color;
        if (Input.GetKeyDown("d") || Input.GetKeyDown("e") || Input.GetKeyDown("c"))
        {
            // col.r = 0.8f;
            launcherPoints[2].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Bounce");
        }
        // if (Input.GetKeyUp("d"))
        // {
        //     col.r = 0.2f;
        // }
        // launcherPoints[2].GetComponent<SpriteRenderer>().color = col;
    }

    private void SpawnInCol(int col)
    {
        // Rows are arranged in the scene such that zero is in the middle.
        int row = Random.Range(0, song.rows);
        int bag = row * 3 + col;
        GameObject newbaby = Instantiate(baby, spawnPoints[col]
                                        .transform.position, Quaternion.identity);
        BabyController controller = newbaby.GetComponent<BabyController>();
        controller.bag = bag;
        controller.beatsUntilLaunch = song.beatsPerMeasure;
    }

    void SpawnBabies(int count)
    {
        if (song.columns == 1)
        {
            SpawnInCol(1);
        }
        else if (song.columns == 2)
        {
            if (count == 1)
            {
                SpawnInCol(Random.Range(1, 3));
            }
            else if (count >= 2)
            {
                SpawnInCol(1);
                SpawnInCol(2);
            }
        }
        else
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
            }
            else if (count == 1)
            {
                SpawnInCol(Random.Range(0, 3));
            }
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
        return timeInSeconds * song.bpm / 60.0;
    }

    public void IncScore(int score)
    {
        GameObject globalVariableHolder = GameObject.Find("GlobalVariableHolder");
        int oldscore = globalVariableHolder.GetComponent<GlobalVariableHolder>().score;
        int newscore = oldscore + score;
        songBeatText.GetComponent<TextMeshProUGUI>().SetText(newscore.ToString());
        globalVariableHolder.GetComponent<GlobalVariableHolder>().score = newscore;
    }
}
