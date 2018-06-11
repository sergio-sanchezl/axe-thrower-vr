using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorizerManager : MonoBehaviour
{
	[System.Serializable]
	public class ColorInfo {
		public Color color;
		public string colorKeyString;
		public int colorIndex;
	}
    public Color[] colorsUsed;

    public ColorInfo minusOneColor;
    public ColorInfo oneColor;
    public ColorInfo twoColor;
    public ColorInfo bonusColor;

    // Use this for initialization
    void Start()
    {
		InitializeColorInfo(minusOneColor);
		InitializeColorInfo(oneColor);
		InitializeColorInfo(twoColor);
		InitializeColorInfo(bonusColor);
    }

	private void InitializeColorInfo(ColorInfo colorInfo) {
		int index = PlayerPrefs.GetInt(colorInfo.colorKeyString + "_index", -1);
		if(index == -1) {
			colorInfo.color = colorsUsed[colorInfo.colorIndex % colorsUsed.Length];
			SaveColorToPlayerPrefs(colorInfo);
		} else {
			colorInfo.colorIndex = index;
			colorInfo.color = GetColorFromPlayerPrefs(colorInfo.colorKeyString);
		}
	}
	private void SaveColorToPlayerPrefs(ColorInfo colorInfo) {
		PlayerPrefs.SetInt(colorInfo.colorKeyString + "_index", colorInfo.colorIndex);
		PlayerPrefs.SetString(colorInfo.colorKeyString, colorInfo.color.ToString());
	}
	private Color GetColorFromPlayerPrefs(string key)
    {
        string colorString;
        colorString = PlayerPrefs.GetString(key, "RGBA(1,1,1,1)");
        //Remove the header and brackets
        colorString = colorString.Replace("RGBA(", "");
        colorString = colorString.Replace(")", "");

        //Get the individual values (red green blue and alpha)
        var strings = colorString.Split(","[0]);

        Color outputColor;
        outputColor = Color.black; // initialize to 0 0 0 1.
        for (var i = 0; i < 4; i++)
        {
            outputColor[i] = System.Single.Parse(strings[i]);
        }
		return outputColor;
    }

	public void ChangeToNextColor(ColorInfo colorInfo) {
		colorInfo.colorIndex = (colorInfo.colorIndex + 1) % colorsUsed.Length;
		colorInfo.color = colorsUsed[colorInfo.colorIndex];
		SaveColorToPlayerPrefs(colorInfo);
	}
}
