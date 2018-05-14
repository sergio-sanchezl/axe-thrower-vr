using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    TextToSpeech tts;
    void Start()
    {

        tts = GetComponent<TextToSpeech>();
        tts.SetLanguage(TextToSpeech.Locale.ES);
    }
    public void Speak()
    {
        tts.Speak("¡Hola, me llamo Sergio! Un placer.", (string msg) =>
        {
            tts.ShowToast(msg);
        });
    }
    public void ChangeSpeed()
    {
        tts.SetSpeed(0.5f);
    }
    public void ChangeLanguage()
    {
        tts.SetLanguage(TextToSpeech.Locale.ES);
    }
    public void ChangePitch()
    {
        tts.SetPitch(0.6f);
    }
}
