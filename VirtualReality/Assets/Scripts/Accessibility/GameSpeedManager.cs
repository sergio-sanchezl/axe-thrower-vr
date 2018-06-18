using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedManager : MonoBehaviour {

	public bool reducedSpeedModeActivated;
	// Use this for initialization
	void Start () {
		this.reducedSpeedModeActivated = PlayerPrefs.GetInt("reduced_speed_mode", 0) == 1;
	}

	public bool ToggleReducedSpeedMode() {
		ChangeReducedSpeedMode(!reducedSpeedModeActivated);
		return this.reducedSpeedModeActivated;
	}
	public void ChangeReducedSpeedMode(bool value) {
		this.reducedSpeedModeActivated = value;
		PlayerPrefs.SetInt("reduced_speed_mode", (value) ? 1 : 0);
	}
}
