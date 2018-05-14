using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to emit the collision events to the parent.
public class CollisionEmitterToParent : MonoBehaviour {

    IChildsCollisionReceiver parentScript;
	// Use this for initialization
	void Start () {
        // the parent script that implements collision must implement IChildsCollisionReceiver.
		this.parentScript = this.transform.parent.GetComponent(typeof (IChildsCollisionReceiver)) as IChildsCollisionReceiver;
	}

    void OnCollisionEnter(Collision other)
    {
        parentScript.ReceiveCollisionEnter(other);
    }

    void OnCollisionExit(Collision other)
    {
        parentScript.ReceiveCollisionExit(other);
    }

    void OnCollisionStay(Collision other)
    {
        parentScript.ReceiveCollisionStay(other);
    }
}
