using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionScheduler : MonoBehaviour {

	public IEnumerator DestroyAfterTime(float seconds) {
		yield return new WaitForSeconds(seconds);
		Destroy(this.gameObject);
		yield return null;
	}
}
