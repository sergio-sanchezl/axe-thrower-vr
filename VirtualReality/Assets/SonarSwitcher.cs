using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarSwitcher : MonoBehaviour {

	public bool sonarActivated;
    // Use this for initialization
    void Start()
    {
        this.sonarActivated = PlayerPrefs.GetInt("sonar", 0) == 1;
    }

    public bool ToggleSonar()
    {
        ChangeSonar(!sonarActivated);
        return this.sonarActivated;
    }
    public void ChangeSonar(bool value)
    {
        this.sonarActivated = value;
        PlayerPrefs.SetInt("sonar", (value) ? 1 : 0);
    }
}
