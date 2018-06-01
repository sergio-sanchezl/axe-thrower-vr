using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameEndManager : MonoBehaviour
{

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private TargetSpawner targetSpawner;
    [SerializeField] private GameObject throwingAxeHand;

    [SerializeField] private GameObject compass;

    [SerializeField] private GameObject canvas;

    [SerializeField] private Text resultsText;

    [SerializeField] private Text recordText;

    public void TriggerMatchEnd()
    {
        int score = scoreManager.GetPoints();
        int time = timerManager.GetSecondsElapsed();
        int recordTime = PlayerPrefs.GetInt("record_time", -1);
        int recordScore = PlayerPrefs.GetInt("record_score", -1);
        targetSpawner.active = false;
        throwingAxeHand.SetActive(false);
        compass.SetActive(false);

        canvas.SetActive(true);

        resultsText.text = "¡Has conseguido " + score + " puntos en un tiempo de " + ParseSecondsToString(time) + "!";
        if (recordScore == -1)
        {
            // There was no previous record.
            recordText.text = "El resultado se ha añadido al record del juego.";
            PlayerPrefs.SetInt("record_time", time);
            PlayerPrefs.SetInt("record_score", score);
        }
        else
        {
            // There was a record.
            if (score > recordScore)
            {
                // We have beaten the record!
                recordText.text = "¡Has batido el record de " + recordScore + " puntos en un tiempo de " + ParseSecondsToString(recordTime) + "!";
                PlayerPrefs.SetInt("record_time", time);
                PlayerPrefs.SetInt("record_score", score);
            }
            else
            {
                // We have NOT beaten the record.
                recordText.text = "No has batido el record de " + recordScore + " puntos en un tiempo de " + ParseSecondsToString(recordTime) + ".";
            }
        }

        Debug.Log("Finished match! Score: " + score + " · Time (in seconds): " + time);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    string ParseSecondsToString(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }
}
