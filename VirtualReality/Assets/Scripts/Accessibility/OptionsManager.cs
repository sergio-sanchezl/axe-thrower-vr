using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public TextToSpeech tts;
    public ColorizerManager colorizerManager;
    public MonoStereoSwitcher monoStereoSwitcher;
    public MenuManager menuManager;
    public HighContrastModeManager highContrastModeManager;
    public GameSpeedManager gameSpeedManager;
    public GameAreaManager gameAreaManager;
    public LongPressDurationManager longPressDurationManager;
    public FontScaleManager fontScaleManager;
    public SonarSwitcher sonarSwitcher;
    public bool ttsEnabled;
    public Image minusOnePointTargetColorDisplay;
    public Image onePointTargetColorDisplay;
    public Image twoPointsTargetColorDisplay;
    public Image bonusTargetColorDisplay;

    public Text monoStereoDisplay;
    public AlternativeText monoStereoAltText;

    public Text focusModeDisplay;
    public AlternativeText focusModeAltText;

    public Text subtitlesDisplay;
    public AlternativeText subtitlesAltText;

    public Text highContrastDisplay;
    public AlternativeText highContrastAltText;

    public Text gameSpeedDisplay;
    public AlternativeText gameSpeedAltText;

    public Text gameAreaDisplay;
    public AlternativeText gameAreaAltText;

    public Text longPressDisplay;
    public AlternativeText longPressAltText;

    public Text sweepModeDisplay;
    public AlternativeText sweepModeAltText;

    public Text sonarDisplay;
    public AlternativeText sonarAltText;



    // Use this for initialization
    void Start()
    {
        UpdateMinusOnePointTargetColorDisplay();
        UpdateOnePointTargetColorDisplay();
        UpdateTwoPointsTargetColorDisplay();
        UpdateBonusTargetColorDisplay();
        UpdateMonoStereoDisplay();
        UpdateHighContrastDisplay();
        UpdateGameSpeedDisplay();
        UpdateGameAreaDisplay();
        UpdateLongPressDisplay();
        UpdateSubtitlesDisplay();
        UpdateFocusModeDisplay();
        UpdateSweepModeDisplay();
        UpdateSonarDisplay();
    }

    public void ReadPhrase(string phrase)
    {
        if (this.ttsEnabled)
        {
            tts.Speak(phrase);
        }
    }

    public void ChangeSweepMode()
    {
        bool active = this.menuManager.ToggleSweepMode();
        UpdateSweepModeDisplay();
        UpdateFocusModeDisplay();
        string ttsPhrase;
        if (active)
        {
            ttsPhrase = "Se ha activado el modo de barrido";
        }
        else
        {
            ttsPhrase = "Se ha desactivado el modo de barrido";
        }
        ReadPhrase(ttsPhrase);
    }

    public void UpdateSweepModeDisplay()
    {
        string status = (this.menuManager.sweepModeEnabled) ? "Activado" : "No activado";
        this.ttsEnabled = this.menuManager.focusMode || this.menuManager.sweepModeEnabled;
        sweepModeDisplay.text = status;
        sweepModeAltText.ChangeAltTextAndNotifyPanel("Botón: Alternar modo de navegación por barrido. Actual: " + status);
    }

    public void ChangeHighContrast()
    {
        bool active = this.highContrastModeManager.ToggleHighContrastMode();
        UpdateHighContrastDisplay();
        string ttsPhrase;
        if (active)
        {
            ttsPhrase = "Se ha activado el modo de alto contraste";
        }
        else
        {
            ttsPhrase = "Se ha desactivado el modo de alto contraste";
        }
        ReadPhrase(ttsPhrase);
    }

    public void UpdateHighContrastDisplay()
    {
        string status = (this.highContrastModeManager.highContrastModeEnabled) ? "Activado" : "No activado";
        highContrastDisplay.text = status;
        highContrastAltText.ChangeAltTextAndNotifyPanel("Botón: Alternar modo de alto contraste. Actual: " + status);
    }
    public void ChangeGameSpeed()
    {
        bool reduced = this.gameSpeedManager.ToggleReducedSpeedMode();
        UpdateGameSpeedDisplay();
        string ttsPhrase;
        if (reduced)
        {
            ttsPhrase = "Se ha reducido la velocidad del juego";
        }
        else
        {
            ttsPhrase = "Se ha reestablecido la velocidad del juego";
        }
        ReadPhrase(ttsPhrase);
    }

    public void UpdateGameSpeedDisplay()
    {
        string status = (this.gameSpeedManager.reducedSpeedModeActivated) ? "Reducida" : "Normal";
        gameSpeedDisplay.text = status;
        gameSpeedAltText.ChangeAltTextAndNotifyPanel("Botón: Cambiar velocidad del juego. Actual: " + status);
    }
    public void ChangeGameArea()
    {
        bool reduced = this.gameAreaManager.ToggleReducedAreaMode();
        UpdateGameAreaDisplay();
        string ttsPhrase;
        if (reduced)
        {
            ttsPhrase = "Se ha reducido la amplitud de la zona de juego";
        }
        else
        {
            ttsPhrase = "Se ha reestablecido la amplitud de la zona de juego";
        }
        ReadPhrase(ttsPhrase);
    }

    public void UpdateGameAreaDisplay()
    {
        string status = (this.gameAreaManager.reducedArea) ? "Estrecha" : "Amplia";
        gameAreaDisplay.text = status;
        gameAreaAltText.ChangeAltTextAndNotifyPanel("Botón: Cambiar amplitud de zona de juego. Actual: " + status);
    }
    public void ChangeLongPress()
    {
        bool longer = this.longPressDurationManager.ToggleLongPressDuration();
        UpdateLongPressDisplay();
        string ttsPhrase;
        if (longer)
        {
            ttsPhrase = "Se ha cambiado el tiempo necesario para pulsación larga a un segundo";
        }
        else
        {
            ttsPhrase = "Se ha cambiado el tiempo necesario para pulsación larga a medio segundo";
        }
        ReadPhrase(ttsPhrase);
    }

    public void UpdateLongPressDisplay()
    {
        string status = (this.longPressDurationManager.longerPress) ? "1000" : "500";
        longPressDisplay.text = status + " ms";
        longPressAltText.ChangeAltTextAndNotifyPanel("Botón: Cambiar duración de pulsado largo. Actual: " + status + " milisegundos");
    }
    public void UpdateSubtitlesDisplay()
    {
        string status = (SubtitleManager.AreSubtitlesEnabled()) ? "Activados" : "No activados";
        subtitlesDisplay.text = status;
        subtitlesAltText.ChangeAltTextAndNotifyPanel("Botón: Alternar subtítulos. Actual: " + status);
    }
    public void ChangeSubtitles()
    {
        bool subtitles = SubtitleManager.ToggleSubtitles();
        UpdateSubtitlesDisplay();
        string ttsPhrase;
        if (subtitles)
        {
            ttsPhrase = "Se han activado los subtítulos";
        }
        else
        {
            ttsPhrase = "Se han desactivado los subtítulos";
        }
        ReadPhrase(ttsPhrase);
    }

    public void UpdateSonarDisplay()
    {
        string status = (sonarSwitcher.sonarActivated) ? "Activado" : "Desactivado";
        sonarDisplay.text = status;
        sonarAltText.ChangeAltTextAndNotifyPanel("Botón: Alternar sónar. Actual: " + status);
    }
    public void ChangeSonar()
    {
        bool sonar = sonarSwitcher.ToggleSonar();
        UpdateSonarDisplay();
        string ttsPhrase;
        if (sonar)
        {
            ttsPhrase = "Se ha activado el sónar";
        }
        else
        {
            ttsPhrase = "Se ha desactivado el sónar";
        }
        ReadPhrase(ttsPhrase);
    }


    public void UpdateFocusModeDisplay()
    {
        string status = (this.menuManager.focusMode) ? "Activado" : "No activado";
        this.ttsEnabled = this.menuManager.focusMode || this.menuManager.sweepModeEnabled; // options' menu local TTS will be ON if either the focus or sweep modes are enabled.
        focusModeDisplay.text = status;
        focusModeAltText.ChangeAltTextAndNotifyPanel("Botón: Alternar modo de navegación por foco. Actual: " + status);
    }

    public void ChangeFocusMode()
    {
        bool focusMode = this.menuManager.ToggleFocusMode();
        UpdateFocusModeDisplay();
        UpdateSweepModeDisplay();
        string ttsPhrase;
        if (focusMode)
        {
            ttsPhrase = "Se ha activado el modo de navegación por foco";
        }
        else
        {
            ttsPhrase = "Se ha desactivado el modo de navegación por foco";
        }
        ReadPhrase(ttsPhrase);
    }
    public void UpdateMonoStereoDisplay()
    {
        string status = (this.monoStereoSwitcher.stereoActivated) ? "Estéreo" : "Mono";
        monoStereoDisplay.text = status;
        monoStereoAltText.ChangeAltTextAndNotifyPanel("Botón: Cambiar modo de audio. Actual: " + status);
    }

    public void ChangeMonoStereo()
    {
        bool stereo = this.monoStereoSwitcher.ToggleMonoStereo();
        UpdateMonoStereoDisplay();
        string ttsPhrase;
        if (stereo)
        {
            ttsPhrase = "Se ha cambiado el modo de audio a estéreo";
        }
        else
        {
            ttsPhrase = "Se ha cambiado el modo de audio a mono";
        }
        ReadPhrase(ttsPhrase);
    }

    public void UpdateMinusOnePointTargetColorDisplay()
    {
        minusOnePointTargetColorDisplay.color = colorizerManager.minusOneColor.color;
    }

    public void UpdateOnePointTargetColorDisplay()
    {
        onePointTargetColorDisplay.color = colorizerManager.oneColor.color;
    }

    public void UpdateTwoPointsTargetColorDisplay()
    {
        twoPointsTargetColorDisplay.color = colorizerManager.twoColor.color;
    }

    public void UpdateBonusTargetColorDisplay()
    {
        bonusTargetColorDisplay.color = colorizerManager.bonusColor.color;
    }
    public void ChangeMinusOnePointTargetColor()
    {
        colorizerManager.ChangeToNextColor(colorizerManager.minusOneColor);
        UpdateMinusOnePointTargetColorDisplay();
        ReadPhrase("Se ha cambiado el color de las dianas de menos un punto");
    }

    public void ChangeOnePointTargetColor()
    {
        colorizerManager.ChangeToNextColor(colorizerManager.oneColor);
        UpdateOnePointTargetColorDisplay();
        ReadPhrase("Se ha cambiado el color de las dianas de un punto");
    }

    public void ChangeTwoPointsTargetColor()
    {
        colorizerManager.ChangeToNextColor(colorizerManager.twoColor);
        UpdateTwoPointsTargetColorDisplay();
        ReadPhrase("Se ha cambiado el color de las dianas de dos puntos");
    }
    public void ChangeBonusTargetColor()
    {
        colorizerManager.ChangeToNextColor(colorizerManager.bonusColor);
        UpdateBonusTargetColorDisplay();
        ReadPhrase("Se ha cambiado el color de las dianas de bonus");
    }

    public void IncrementFontScale() {
        this.fontScaleManager.IncrementScale();
        ReadPhrase("Se ha incrementado la escala de fuente");
    }

    public void DecrementFontScale() {
        this.fontScaleManager.DecrementScale();
        ReadPhrase("Se ha reducido la escala de fuente");        
    }
}
