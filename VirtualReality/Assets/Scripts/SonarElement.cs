using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarElement : MonoBehaviour {

	[SerializeField] private AudioClip audioClip;
	private SonarManager sonarManager;
	
	void Start () {
		this.sonarManager = GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>();
	}
	
	public void PlaySound() {
		sonarManager.PlaySound(audioClip);
	}

	public void StopSound() {
		sonarManager.StopSound();
	}
}
