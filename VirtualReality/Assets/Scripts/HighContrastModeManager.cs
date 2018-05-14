using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighContrastModeManager : MonoBehaviour
{

    public bool highContrastModeEnabled;

    public Light directionalLight;

    // Use this for initialization
    void Start()
    {
		Debug.Log(RenderSettings.skybox.name);
        bool storedValue = PlayerPrefs.GetInt("high_contrast_mode", 0) == 1 ? true : false;
		ChangeHighContrastMode(storedValue);
    }

	public void ToggleHighContrastMode() {
		ChangeHighContrastMode(!highContrastModeEnabled);
	}
    public void ChangeHighContrastMode(bool value)
    {
        Color fogColor = (value) ? Color.black : new Color(61 / 255f, 97 / 255f, 231 / 255f);
        float skyboxExposureValue = (value) ? 0f : 1.45f;
        Color directionalLightColor = (value) ? Color.black : Color.white;

		Debug.Log("Before changing the fogColor to " + fogColor.ToString() + ": " + RenderSettings.fogColor.ToString());
        RenderSettings.fogColor = fogColor;
		Debug.Log("After changing the fogColor to " + fogColor.ToString() + ": " + RenderSettings.fogColor.ToString());
		// Debug.Log("Before chaging the _Exposure value to " + skyboxExposureValue + ": " + RenderSettings.skybox.GetFloat("_Exposure"));
        RenderSettings.skybox.SetFloat("_Exposure", skyboxExposureValue);
		// Debug.Log("After chaging the _Exposure value: " + RenderSettings.skybox.GetFloat("_Exposure"));
        directionalLight.color = directionalLightColor;
		DynamicGI.UpdateEnvironment();
        this.highContrastModeEnabled = value;

		PlayerPrefs.SetInt("high_contrast_mode", value ? 1 : 0);
    }


}
