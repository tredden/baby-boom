using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HelperButton : MonoBehaviour
{
    private GameObject globalVariableHolder;
    // Start is called before the first frame update
    void Start()
    {
        globalVariableHolder = GameObject.Find("GlobalVariableHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if(globalVariableHolder.GetComponent<GlobalVariableHolder>().showLetters){
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "KeyHelper: ON";
            ColorBlock col = transform.GetComponent<Button>().colors;
            col.normalColor = new Color(0.0f,1.0f,0.0f);
            transform.GetComponent<Button>().colors = col;
            
        } else {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "KeyHelper: OFF";
            ColorBlock col = transform.GetComponent<Button>().colors;
            col.normalColor = new Color(1.0f,0.0f,0.0f);
            transform.GetComponent<Button>().colors = col;
        }
    }

    public void ToggleHelper(){
        globalVariableHolder.GetComponent<GlobalVariableHolder>().showLetters = !globalVariableHolder.GetComponent<GlobalVariableHolder>().showLetters;
        
    }
}
