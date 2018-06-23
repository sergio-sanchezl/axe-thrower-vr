using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayOnAwakeFix : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		
	}

	void Start () {
		this.gameObject.SetActive(false);
		Invoke("Reactivate",0.05f);
		// AudioSource ausrc = this.GetComponent<AudioSource>();
		// ausrc.Stop();
		// ausrc.Play();
	}

	void Reactivate() {
		this.gameObject.SetActive(true);
	}
}
