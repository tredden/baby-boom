using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkController : MonoBehaviour
{
    public GameObject main;
    private MainController mainController;
    private double lastTimeToNextBeat;
    private double timeToNextBeat;
    private int currBeat;
    // Start is called before the first frame update
    private bool fate;
    void Start()
    {
        main = GameObject.Find("MainController");
        mainController = main.GetComponent<MainController>();
        fate = Random.Range(0,2)==0;
        currBeat = 0;
        lastTimeToNextBeat=-1;

    }

    // Update is called once per frame
    void Update()
    {
        timeToNextBeat = mainController.GetNextBeat();
        if (lastTimeToNextBeat < timeToNextBeat)
        {
            currBeat++;
            if (currBeat % 1 == 0)
            {
                transform.GetComponent<Animator>().SetBool("isDancing",fate);
                fate=!fate;
            }
        }
        lastTimeToNextBeat = timeToNextBeat;
    }
}
