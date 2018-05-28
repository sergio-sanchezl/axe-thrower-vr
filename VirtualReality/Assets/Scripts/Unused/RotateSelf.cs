using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {

    public Transform rotateAround;
    public float speed = 10F;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(rotateAround != null)
        {
            this.transform.RotateAround(rotateAround.position, Vector3.up, 1f * speed / Time.deltaTime);
        }
        
    }
}
