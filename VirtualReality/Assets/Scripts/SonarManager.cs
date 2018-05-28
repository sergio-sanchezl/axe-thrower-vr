using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarManager : MonoBehaviour
{

    private AudioSource audioSource;
    void Start() {
        this.audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip audioClip)
    {
		// // lazy load of audioSource.
		// if(this.audioSource == null) {
		// 	this.audioSource = GetComponent<AudioSource>();
		// 	this.audioSource.enabled = true;
		// }
		StopSound();
        if (!this.audioSource.isPlaying)
        {
            this.audioSource.clip = audioClip;
            this.audioSource.loop = true;
            this.audioSource.Play();
        }
    }

	public void StopSound() {
		this.audioSource.loop = false;
		this.audioSource.Stop();
	}


}
