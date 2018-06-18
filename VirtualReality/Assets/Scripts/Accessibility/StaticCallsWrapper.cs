using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCallsWrapper : MonoBehaviour {

	public void CallToggleSubtitles() {
		SubtitleManager.ToggleSubtitles();
	}
}
