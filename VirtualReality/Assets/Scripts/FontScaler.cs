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
	void Awake () {
		// Debug.Log("Font scaler AWAKE()");
		this.text = GetComponent<Text>();
		this.baseFontSize = text.fontSize;
		this.fontScaleManager.AddObserver(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Notify() {
		Debug.Log("Font scaler NOTIFY!");
		this.text.fontSize = Mathf.RoundToInt(this.fontScaleManager.GetScale() * baseFontSize);
		this.currentSize = this.text.fontSize;
	}

	void OnEnable()
	{
		// Debug.Log("On Enable Font Scaler");
		this.text.fontSize = Mathf.RoundToInt(this.fontScaleManager.GetScale() * baseFontSize);	
		this.currentSize = this.text.fontSize;	
	}
}
