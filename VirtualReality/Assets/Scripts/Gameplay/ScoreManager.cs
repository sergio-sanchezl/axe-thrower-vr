using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public bool speechEnabled = false;
    public TextToSpeech tts;
    private int points;
    public Text pointsMarker;

    public float pointsMultiplier = 1f;

    public Object fadingTextPrefab;
    public GameObject scoreContainer;
    // Use this for initialization
    void Start()
    {
        this.points = 0;
		this.pointsMarker.text = "" + this.points;
        this.speechEnabled = PlayerPrefs.GetInt("focus_mode", 0) == 1;
    }

    public void AddPoints(int pointsToAdd)
    {
        this.AddPoints(pointsToAdd, true);
    }
    public void AddPoints(int pointsToAdd, bool useMultiplier)
    {
        // Increment the current points by the points to be added.
        // If we have to use the multiplier, then multiply and ceil the 
        // points to add by the multiplier.
        Debug.Log("pointsToAdd: " + pointsToAdd + ", applying multiplier: " + (pointsToAdd * pointsMultiplier) + ", ceil to int: " + Mathf.CeilToInt(pointsToAdd * pointsMultiplier));
        int addedPoints = (useMultiplier) ? Mathf.CeilToInt(pointsToAdd * pointsMultiplier) : pointsToAdd;
        this.points += addedPoints;
        // Update the UI points' display
        this.pointsMarker.text = "" + this.points;

        // Display the UI element that indicates how many points we have earned with this call.
        GameObject fadingText = Instantiate(fadingTextPrefab) as GameObject;
        FadingMovingText fadingTextScript = fadingText.GetComponent<FadingMovingText>();
        fadingTextScript.value = (addedPoints >= 0) ? "+" + addedPoints : "" + addedPoints;
        fadingTextScript.positive = addedPoints >= 0;
        fadingText.transform.SetParent(scoreContainer.transform, false);

        // 
        if (speechEnabled && tts != null)
        {
            tts.SetSpeed(1.5f);
            string spokenText = "";
            if(this.points < 0) {
                int positivePoints = -points;
                spokenText = "Tienes menos " + ((positivePoints) == 1 ? "un punto" : positivePoints + " puntos");
            } else {
                spokenText = "Tienes " + ((this.points) == 1 ? "un punto" : this.points + " puntos");
            }
            tts.Speak(spokenText);
            // tts.SetSpeed(1.0f);
        }
    }

    public void SetMultiplier(float value)
    {
        this.pointsMultiplier = value;
    }

    public int GetPoints() {
        return points;
    }
}
