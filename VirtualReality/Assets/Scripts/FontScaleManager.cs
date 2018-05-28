using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontScaleManager : MonoBehaviour {

	public float scale = 1f;
	public float minimumScale = 0.5f;
	public float maximumScale = 2f;
	public float stepScale = 0.25f;
	public ArrayList observers = new ArrayList();
	void Start() {
		this.scale = PlayerPrefs.GetFloat("font_scale", 1f);
	}

	public void IncrementScale() {
		ChangeScale(stepScale);
	}
	public void DecrementScale() {
		ChangeScale(-stepScale);
	}
	public void ChangeScale(float difference) {
		SetScale(this.scale + difference);
	}

	public void SetScale(float value) {
		this.scale = Mathf.Clamp(value, minimumScale, maximumScale);
		PlayerPrefs.SetFloat("font_scale", this.scale);
		this.NotifyAll();
		Debug.Log("Setting scale to " + value);
	}

	public float GetScale() {
		return this.scale;
	}

	public void AddObserver(FontScaler observer) {
		this.observers.Add(observer);
		observer.Notify();
	}

	public void NotifyAll() {
		Debug.Log("notifying all");
		foreach (var observer in observers)
		{
			((FontScaler) observer).Notify();
		}
	}
}
