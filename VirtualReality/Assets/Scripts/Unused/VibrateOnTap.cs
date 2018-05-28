using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateOnTap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Handheld.Vibrate();
	}
}
