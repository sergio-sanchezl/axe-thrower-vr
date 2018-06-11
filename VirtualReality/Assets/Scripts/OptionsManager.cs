using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {
	public TextToSpeech tts;
	public ColorizerManager colorizerManager;
	public MonoStereoSwitcher monoStereoSwitcher;
	public Image minusOnePointTargetColorDisplay;
	public Image onePointTargetColorDisplay;
	public Image twoPointsTargetColorDisplay;
	public Image bonusTargetColorDisplay;

	public Text monoStereoDisplay;
	public AlternativeText monoStereoAltText;

	// Use this for initialization
	void Start () {
		UpdateMinusOnePointTargetColorDisplay();
		UpdateOnePointTargetColorDisplay();
		UpdateTwoPointsTargetColorDisplay();
		UpdateBonusTargetColorDisplay();
		UpdateMonoStereoDisplay();
	}

	public void UpdateMonoStereoDisplay() {
		monoStereoDisplay.text = (this.monoStereoSwitcher.stereoActivated) ? "Estéreo" : "Mono";
		monoStereoAltText.altText = "Botón: Cambiar modo de audio. Actual: " + ((this.monoStereoSwitcher.stereoActivated) ? "Estéreo" : "Mono");
	}

	public void ChangeMonoStereo() {
		bool stereo = this.monoStereoSwitcher.ToggleMonoStereo();
		UpdateMonoStereoDisplay();
		string ttsPhrase;
		if(stereo) {
			ttsPhrase = "Se ha cambiado el modo de audio a estéreo";
		} else {
			ttsPhrase = "Se ha cambiado el modo de audio a mono";
		}
		// READ HERE THE TTS PHRASE
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
