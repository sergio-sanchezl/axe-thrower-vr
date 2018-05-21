using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public bool speechEnabled = false;
    public TextToSpeech tts;
    public int points;
    public Text pointsMarker;

    public float pointsMultiplier = 1f;
    // Use this for initialization
    void Start()
    {
        this.points = 0;
		this.pointsMarker.text = "" + this.points;
    }

    public void AddPoints(int pointsToAdd)
    {
        this.AddPoints(pointsToAdd, false);
    }
    public void AddPoints(int pointsToAdd, bool useMultiplier)
    {
        // Increment the current points by the points to be added.
        // If we have to use the multiplier, then multiply and ceil the 
        // points to add by the multiplier.
        this.points += (useMultiplier) ? Mathf.CeilToInt(pointsToAdd * pointsMultiplier) : pointsToAdd;
        // Update the UI points' display
        this.pointsMarker.text = "" + this.points;
        // 
        if (speechEnabled && tts != null)
        {
            tts.Speak(this.points + " puntos");
        }
    }

    public void SetMultiplier(float value)
    {
        this.pointsMultiplier = value;
    }
}
