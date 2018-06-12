using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPressDurationManager : MonoBehaviour
{

    public bool longerPress;

    public MenuManager menuManager;
    // Use this for initialization
    void Start()
    {
        this.longerPress = PlayerPrefs.GetInt("longer_long_press_duration", 0) == 1;
    }

    public bool ToggleLongPressDuration()
    {
        ChangeLongPressDuration(!longerPress);
        return this.longerPress;
    }
    public void ChangeLongPressDuration(bool value)
    {
        this.longerPress = value;
        PlayerPrefs.SetInt("longer_long_press_duration", (value) ? 1 : 0);
        if (menuManager != null)
        {
            menuManager.SetLongPressDuration(value);
        }

    }
}
