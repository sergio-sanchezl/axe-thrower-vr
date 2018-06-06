using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarManager : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;

    public void PlaySound(AudioClip audioClip, float pitch, float volume) {
        Debug.Log("Play Sound called!");
        StopSound();
        if (!this.audioSource.isPlaying)
        {
            this.audioSource.clip = audioClip;
            this.audioSource.pitch = pitch;
            this.audioSource.volume = volume;
            this.audioSource.loop = true;
            this.audioSource.Play();
        }
    }
    public void PlaySound(AudioClip audioClip)
    {
		PlaySound(audioClip, 1f, 1f);
    }

	public void StopSound() {
        // Debug.Log("Stop Sound Called!");
		this.audioSource.loop = false;
		this.audioSource.Stop();
	}

    public void ChangePitchAndVolume(float value) {
        this.audioSource.pitch = value;
        this.audioSource.volume = value;
    }

}
