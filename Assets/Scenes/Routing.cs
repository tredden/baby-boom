using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routing : MonoBehaviour
{
    public AudioClip song1Audio;
    public AudioClip song2Audio;
    public AudioClip song3Audio;

    public void ShowSongsScene()
    {
        // Show the scene named "Songs"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Songs");
    }
    public void ShowMenuScene()
    {
        // Show the scene named "Menu"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void ShowResultsScene()
    {
        // Show the scene named "Results"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Results");
    }
    private void ShowGameScene()
    {
        // Show the scene named "Game"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    private void playSong(SongConfig song)
    {
        // Set the song on the global variable holder
        GameObject globalVariableHolder = GameObject.Find("GlobalVariableHolder");
        globalVariableHolder.GetComponent<GlobalVariableHolder>().song = song;
        globalVariableHolder.GetComponent<GlobalVariableHolder>().score = 0;
        
        // Show the game scene
        ShowGameScene();
    }
    public void song1()
    {
        // Set the song config
        SongConfig song = new SongConfig
        {
            audioClip = song1Audio,
            bpm = 33.333,
            columns = 1,
            rows = 3,
            spawnBabyPeriod = 1,
        };
        playSong(song);
    }
    public void song2()
    {
        // Set the song config
        SongConfig song = new SongConfig
        {
            audioClip = song2Audio,
            bpm = 75,
            columns = 1,
            rows = 3,
            spawnBabyPeriod = 2,
        };
        playSong(song);
    }

    public void song3()
    {
        // Set the song config
        SongConfig song = new SongConfig
        {
            audioClip = song3Audio,
            bpm = 150,
            columns = 1,
            rows = 3,
            spawnBabyPeriod = 2,
        };
        playSong(song);
    }
}
