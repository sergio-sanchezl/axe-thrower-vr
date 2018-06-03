using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TextAndAltText
{
    [SerializeField] private Text text;
    [SerializeField] private AlternativeText altText;

    public void SetText(string text) {
        this.text.text = text;
    }

    public void SetAltText(string altText) {
        this.altText.altText = altText;
    }
}