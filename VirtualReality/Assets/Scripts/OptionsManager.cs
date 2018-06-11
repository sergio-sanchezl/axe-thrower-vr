using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

	public ColorizerManager colorizerManager;

	public Image minusOnePointTargetColorDisplay;
	public Image onePointTargetColorDisplay;
	public Image twoPointsTargetColorDisplay;
	public Image bonusTargetColorDisplay;

	// Use this for initialization
	void Start () {
		UpdateMinusOnePointTargetColorDisplay();
		UpdateOnePointTargetColorDisplay();
		UpdateTwoPointsTargetColorDisplay();
		UpdateBonusTargetColorDisplay();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateMinusOnePointTargetColorDisplay() {
		minusOnePointTargetColorDisplay.color = colorizerManager.minusOneColor.color;
	}

	public void UpdateOnePointTargetColorDisplay() {
		onePointTargetColorDisplay.color = colorizerManager.oneColor.color;
	}

	public void UpdateTwoPointsTargetColorDisplay() {
		twoPointsTargetColorDisplay.color = colorizerManager.twoColor.color;
	}

	public void UpdateBonusTargetColorDisplay() {
		bonusTargetColorDisplay.color = colorizerManager.bonusColor.color;
	}
	public void ChangeMinusOnePointTargetColor() {
		colorizerManager.ChangeToNextColor(colorizerManager.minusOneColor);
		UpdateMinusOnePointTargetColorDisplay();
	}

	public void ChangeOnePointTargetColor() {
		colorizerManager.ChangeToNextColor(colorizerManager.oneColor);
		UpdateOnePointTargetColorDisplay();
	}

	public void ChangeTwoPointsTargetColor() {
		colorizerManager.ChangeToNextColor(colorizerManager.twoColor);
		UpdateTwoPointsTargetColorDisplay();
	}
	public void ChangeBonusTargetColor() {
		colorizerManager.ChangeToNextColor(colorizerManager.bonusColor);
		UpdateBonusTargetColorDisplay();
	}
}
