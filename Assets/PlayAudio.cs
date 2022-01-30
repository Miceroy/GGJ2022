using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    AudioSource audioData;
    // Start is called before the first frame update

    private void OnEnable()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }
    void Start()
    {
       
        //Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
