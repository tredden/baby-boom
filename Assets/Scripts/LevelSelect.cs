using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
	public void PlaySong1() {
        // Log a statement to the console window
        Debug.Log("Play song 1 clicked");
        // Load the next scene in the build order  
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().buildIndex + 1);
    }

}
