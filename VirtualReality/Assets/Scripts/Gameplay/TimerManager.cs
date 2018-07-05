using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerManager : MonoBehaviour
{

    // How many seconds should elapsed for the timer to stop?
    public int timerEnd = 3;

    // The seconds elapsed since the start of the timer.
    private int secondsElapsed = 0;

    // // The actual time when the timer started. Maybe not needed.
    // public int timerStart;

    // UI element for the timer value to be displayed. 
    public Text timerText;

    public GameObject timerContainer;
    public UnityEngine.Object fadingTextPrefab;

    public int secondsToIncreaseDifficulty = 30;

    [SerializeField] private AudioSource sixtySecondsRemaining;
    [SerializeField] private AudioSource thirtySecondsRemaining;

    [SerializeField] private AudioSource belowTenSecondsRemaining;

    [SerializeField] private TargetSpawner targetSpawner;
    [SerializeField] private GameEndManager gameEndManager;
    void Start()
    {
        this.timerText.text = ParseSecondsToString(this.timerEnd - this.secondsElapsed);
        StartCoroutine(TimeLoop());
    }

    IEnumerator TimeLoop()
    {
        while (secondsElapsed < timerEnd)
        {
            yield return new WaitForSeconds(1f);
            AddSecondToTimer();
        }
        PerformEndOfTimerTasks();
        yield return null;
    }

    void AddSecondToTimer()
    {
        this.secondsElapsed += 1;
        if(secondsElapsed % secondsToIncreaseDifficulty == 0) {
            targetSpawner.IncreaseDifficulty();
        }
        UpdateTimer();
    }

    void UpdateTimer()
    {
        int timeDifference = this.timerEnd - this.secondsElapsed;
        this.timerText.text = ParseSecondsToString(timeDifference);
        // TODO: BEEP HERE!
        switch (timeDifference)
        {
            case 60:
                // beep for the minute mark!
                sixtySecondsRemaining.Stop();
                sixtySecondsRemaining.Play();
                break;
            case 30:
                // beep for the half minute mark. uh oh.
                thirtySecondsRemaining.Stop();
                thirtySecondsRemaining.Play();
                break;
            default:
                // beep for the last ten seconds. UH OH!
                if (timeDifference <= 10)
                {
                    belowTenSecondsRemaining.Stop();
                    belowTenSecondsRemaining.Play();
                }
                break;
        }
    }

    string ParseSecondsToString(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }

    string ParseSecondsToStringShort(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D1}:{1:D2}", time.Minutes, time.Seconds);
    }

    internal void ExtendTime(int extraSeconds)
    {
        this.timerEnd += extraSeconds;
        // Display the UI element that indicates how many points we have earned with this call.
        GameObject fadingText = Instantiate(fadingTextPrefab) as GameObject;
        FadingMovingText fadingTextScript = fadingText.GetComponent<FadingMovingText>();
        fadingTextScript.value = (extraSeconds >= 0) ? "+" + ParseSecondsToStringShort(extraSeconds) : "-" + ParseSecondsToStringShort(extraSeconds);
        fadingTextScript.positive = extraSeconds >= 0;
        fadingText.transform.SetParent(timerContainer.transform, false);
        UpdateTimer();
    }

    public int GetSecondsElapsed() {
        return secondsElapsed;
    }

    // Do things that should be done when ending the timer.
    void PerformEndOfTimerTasks()
    {
        this.gameEndManager.TriggerMatchEnd();
    }
}
