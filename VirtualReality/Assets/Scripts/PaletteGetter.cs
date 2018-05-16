using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class PaletteGetter : MonoBehaviour {

	public Color[] palette;
	public Color[] targetColors;
	void Awake() {
		DontDestroyOnLoad(this);
		VisionUtility.GetColorBlindSafePalette(palette, 0.25f, 0.75f);
	}
}
