using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : MonoBehaviour {

	public float rotationSpeed = 1f;
    public float movementSpeed = 1f;
	public Transform whatShouldRotate;
	public float damage = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	virtual public void Update () {
		this.transform.Translate((Vector3.forward * movementSpeed * Time.unscaledDeltaTime), Space.Self);
        whatShouldRotate.Rotate(rotationSpeed * Time.unscaledDeltaTime, 0f, 0f);
	}
}
