using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioController : MonoBehaviour
{

    public AudioSource audioSource;
    public SubtitleManager subtitleManager;
    public string caption;
    public float captionDuration;
    // Use this for initialization
    void Start()
    {
        this.subtitleManager = GameObject.FindGameObjectWithTag("SubtitleManager").GetComponent<SubtitleManager>();
        // if (audioSource == null)
        // {
        //     this.audioSource = GetComponent<AudioSource>();
        // }
    }

    // method to call when we want to play a sound that will also have subtitles if they are enabled.
    public void Play()
    {
        if(this.audioSource.isPlaying) {
            Debug.Log("Audiosource is already playing. what.");
        }
        this.audioSource.Play();
        if(this.audioSource.isPlaying) {
            Debug.Log("Audiosource is already playing. THIS SHOULD BE NORMAL!");
        }
        subtitleManager.DisplaySubtitle(caption, captionDuration);
    }
}
