using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontScaler : MonoBehaviour {

	public FontScaleManager fontScaleManager;
	public int baseFontSize;
	public Text text;

	public int currentSize;
	// Use this for initialization
	void Start () {
		// Debug.Log("Font scaler AWAKE()");
		this.text = GetComponent<Text>();
		this.baseFontSize = text.fontSize;
		this.fontScaleManager.AddObserver(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Notify() {
		Debug.Log("Font scaler NOTIFY!" + this.fontScaleManager.GetScale());
		this.text.fontSize = Mathf.RoundToInt(this.fontScaleManager.GetScale() * baseFontSize);
		this.currentSize = this.text.fontSize;
	}

	// OnEnable is called before Start, so we have to nullcheck.
	void OnEnable()
	{
		// Debug.Log("On Enable Font Scaler");
		if(this.fontScaleManager != null && text != null) {
			this.text.fontSize = Mathf.RoundToInt(this.fontScaleManager.GetScale() * baseFontSize);	
			this.currentSize = this.text.fontSize;	
		}
		
	}
}
