using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationToggler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Coroutine vibrationCoroutine;
    bool vibrating = false;
    public float timeBetweenVibrations = 0.5f; 

    // IEnumerator VibrateEveryXSeconds(float frequency)
    // {
    //     while(vibrating)
    //     {
    //         Handheld.Vibrate();
    //         Debug.Log("Handheld.Vibrate() is called.");
    //         yield return new WaitForSeconds(frequency);
    //     }
        
    // }

    public void ToggleVibration()
    {
        // if(vibrating)
        // {
        //     vibrating = false;
        //     if (vibrationCoroutine != null)
        //     {
        //         StopCoroutine(vibrationCoroutine);
        //         vibrationCoroutine = null;
        //     }
            
        // } else
        // {
        //     vibrating = true;
        //     vibrationCoroutine = StartCoroutine(VibrateEveryXSeconds(timeBetweenVibrations));
            
        // }
    }
}
