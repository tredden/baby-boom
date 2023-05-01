using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public int word;
    void Start()
    {
        StartCoroutine(PlayWord(word));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PlayWord(int word){
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
