using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    // Reference to all the panels that can be shown in this canvas.
    public GameObject[] panels;

    PanelNavigationAccessibility[] panelsScripts;

    GvrPointerGraphicRaycaster raycaster;
    // Id of the currently displayed panel.
    public int currentPanelId = 0;
    // Focus mode refers to the mode that instead of having to aim to the buttons
    // to activate them, the user must just click to move the focus through
    // the elements and then hold to activate them.
    public bool focusMode;
    public bool sweepModeEnabled;
    public float longerLongPressDuration = 1f;
    public float shorterLongPressDuration = 0.5f;

    public bool longerLongPressMode;
    void Awake()
    {
        raycaster = this.transform.GetComponent<GvrPointerGraphicRaycaster>();
        // We fetch the accessibility scripts from each panel.
        panelsScripts = new PanelNavigationAccessibility[panels.Length];
        for (int i = 0; i < panels.Length; i++)
        {
            panelsScripts[i] = panels[i].GetComponent<PanelNavigationAccessibility>();
        }
        // We set the focus mode to the one that is found in
        // the player preferences. it defaults to FALSE (0) if not
        // found.
        // TODO: Might be a good idea to check here if user has
        // talkback enabled on his/her phone.
        SetFocusMode(PlayerPrefs.GetInt("focus_mode", 0) == 1);
        SetSweepMode(PlayerPrefs.GetInt("sweep_mode", 0) == 1);
        longerLongPressMode = PlayerPrefs.GetInt("longer_long_press_duration", 0) == 1;
        SetLongPressDuration(longerLongPressMode);
    }

    // Hides current panel and changes it to the specified one.
    public void ChangePanel(int panelId)
    {
        panels[currentPanelId].SetActive(false);
        this.currentPanelId = panelId;
        panels[panelId].SetActive(true);
    }

    // Change the focus mode to the opposite value.
    public bool ToggleFocusMode()
    {
        SetFocusMode(!focusMode);
        return focusMode;
    }

    public bool ToggleSweepMode() {
        SetSweepMode(!sweepModeEnabled);
        return sweepModeEnabled;
    }

    // We change the focus mode, and write it in the player preferences.
    // Then enable all the accessibility scripts in each panel if 
    // the focus mode is set to TRUE (1), or disable them if the
    // modfe is set to FALSE (0)
    void SetFocusMode(bool value)
    {
        focusMode = value;
        if(value) {
            // we sweep mode + focus mode is not compatible, so we turn sweep mode off if we are activating focus mode.
            sweepModeEnabled = false;
            PlayerPrefs.SetInt("sweep_mode", 0);
            SetPanelScriptsSweepModeEnabled(false);
        }
        PlayerPrefs.SetInt("focus_mode", focusMode ? 1 : 0);
        SetPanelScriptsEnabled(focusMode);
    }

    void SetSweepMode(bool value)
    {
        sweepModeEnabled = value;
        if(value) {
            // sweep mode + focus mode is not compatible. if we activate sweep mode, we deactivate focus mode.
            focusMode = false;
            PlayerPrefs.SetInt("focus_mode", 0);
            SetPanelScriptsEnabled(false);
        }

        PlayerPrefs.SetInt("sweep_mode", sweepModeEnabled ? 1 : 0);
        // If focus mode is disabled, are able to modify the values for the script panels.
        // if not, focus mode must override those values.
        if(!focusMode) {
            SetPanelScriptsEnabled(sweepModeEnabled);
        }
        
        SetPanelScriptsSweepModeEnabled(sweepModeEnabled);        
    }

    void SetPanelScriptsEnabled (bool value) {
        foreach(var panelAccessibility in panelsScripts) {
            panelAccessibility.enabled = value;
        }
        raycaster.enabled = !value;
    }

    void SetPanelScriptsSweepModeEnabled (bool value) {
        foreach(var panelAccessibility in panelsScripts) {
            panelAccessibility.SetSweepMode(value);
        }
    }

    public void SetLongPressDuration(bool longer) {
        SetLongPressDuration((longer) ? longerLongPressDuration : shorterLongPressDuration);
    }
    void SetLongPressDuration(float duration) {
        for (int i = 0; i < panelsScripts.Length; i++)
        {
            panelsScripts[i].ChangeLongPressDuration(duration);
        }
    }


}
