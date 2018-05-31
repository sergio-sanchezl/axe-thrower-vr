using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameEndManager : MonoBehaviour {

	[SerializeField] private ScoreManager scoreManager;
	[SerializeField] private TimerManager timerManager;
	[SerializeField] private TargetSpawner targetSpawner;
	[SerializeField] private GameObject throwingAxeHand;

	[SerializeField] private GameObject compass;

	[SerializeField] private GameObject canvas;

	[SerializeField] private Text resultsText;

	[SerializeField] private Text recordText;

 	public void TriggerMatchEnd() {
		int score = scoreManager.GetPoints();	
		int time = timerManager.GetSecondsElapsed();

		targetSpawner.active = false;
		throwingAxeHand.SetActive(false);
		compass.SetActive(false);

		canvas.SetActive(true);
		Debug.Log("Finished match! Score: " + score + " · Time (in seconds): " + time);
	}

	public void GoBackToMenu() {
		SceneManager.LoadScene("Menu");
	}
}
