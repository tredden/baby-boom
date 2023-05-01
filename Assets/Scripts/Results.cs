using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Results : MonoBehaviour
{
    private GameObject globalVariableHolder;
    // Start is called before the first frame update
    void Start()
    {
        globalVariableHolder = GameObject.Find("GlobalVariableHolder");
        transform.GetComponent<TextMeshProUGUI>().text = "Score: " + globalVariableHolder.GetComponent<GlobalVariableHolder>().score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
