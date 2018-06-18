using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleAudio()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        } else
        {
            audioSource.Play();
        }
    }
}
