using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaManager : MonoBehaviour {

	public bool reducedArea;
	// Use this for initialization
	void Start () {
		this.reducedArea = PlayerPrefs.GetInt("reduced_game_area", 0) == 1;
	}
	
	public bool ToggleReducedAreaMode() {
		ChangeReducedAreaMode(!reducedArea);
		return this.reducedArea;
	}
	public void ChangeReducedAreaMode(bool value) {
		this.reducedArea = value;
		PlayerPrefs.SetInt("reduced_game_area", (value) ? 1 : 0);
	}
}
