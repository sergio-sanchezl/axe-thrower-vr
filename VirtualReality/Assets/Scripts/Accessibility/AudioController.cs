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
    void Awake()
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
        this.audioSource.Play();
        subtitleManager.DisplaySubtitle(caption, captionDuration);
    }
}
