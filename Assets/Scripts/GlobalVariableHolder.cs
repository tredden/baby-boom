using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalVariableHolder : MonoBehaviour
{
    public static GlobalVariableHolder Instance;
    public SongConfig song;
    // Yes, this is cursed, but it's apparently how you pass data from one scene to another.
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
