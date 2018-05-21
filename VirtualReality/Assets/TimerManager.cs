﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerManager : MonoBehaviour
{

    // How many seconds should elapsed for the timer to stop?
    public int timerEnd = 3;

    // The seconds elapsed since the start of the timer.
    public int secondsElapsed = 0;

    // // The actual time when the timer started. Maybe not needed.
    // public int timerStart;

    // UI element for the timer value to be displayed. 
    public Text timerText;

	public EnemySpawnerScript enemySpawner;
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
        int timeDifference = this.timerEnd - this.secondsElapsed;
        this.timerText.text = ParseSecondsToString(timeDifference);
		// TODO: BEEP HERE!
        switch (timeDifference)
        {
            case 60:
				// beep for the minute mark!
                break;
            case 30:
				// beep for the half minute mark. uh oh.
                break;
            default:
				// beep for the last ten seconds. UH OH!
                if (timeDifference <= 10)
                {
					
                }
                break;
        }
    }

    // Do things that should be done when ending the timer.
    void PerformEndOfTimerTasks () {
        enemySpawner.active = false;
    }
    string ParseSecondsToString(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }
}