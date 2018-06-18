using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeText : MonoBehaviour {

	public string altText;
	
	public AccessibilityForUI panel;
	public int index;

	public void ChangeAltTextAndNotifyPanel(string value) {
		this.altText = value;
		if(panel != null) {
			panel.ChangeTextToBeReadByIndex(this.index, this.altText);
		}
	}
}
