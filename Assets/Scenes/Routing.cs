using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routing : MonoBehaviour
{
    public void ShowSongsScene() {
        // Show the scene named "Songs"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Songs");
    }
    public void ShowMenuScene() {
        // Show the scene named "Menu"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void ShowResultsScene() {
        // Show the scene named "Results"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Results");
    }
    public void ShowGameScene() {
        // Show the scene named "Game"
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
