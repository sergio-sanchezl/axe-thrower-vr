using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

	public Object arrowPrefab;
	public List<GameObject> targetsTracking;
	public List<GameObject> arrows;
	void Start () {
		this.targetsTracking = new List<GameObject>();
		this.arrows = new List<GameObject>();
	}

	public void AddTargetToBeTracked(GameObject newTarget) {
		if (!this.targetsTracking.Contains(newTarget)) {
			// Add the target to the list of targets.
			this.targetsTracking.Add(newTarget);
			// Instantiate the new arrow for that target.
			GameObject newArrow = Instantiate(arrowPrefab) as GameObject;
			// Set the arrow to be a child of the compass.
			newArrow.transform.SetParent(this.transform);
			newArrow.transform.localPosition = Vector3.zero;
			CompassArrow compassArrow = newArrow.GetComponent<CompassArrow>();
			// Set the arrow's target.
			compassArrow.SetTarget(newTarget);
			// Add the arrow to the list of arrows.
			this.arrows.Add(newArrow);
		}
	}

	public void DeleteTarget(GameObject target) {
		// Get index where the target is.
		int index = this.targetsTracking.IndexOf(target);
		// Remove the target by index.
		this.targetsTracking.RemoveAt(index);
		// Destroy the arrow for that target.
		Destroy(this.arrows[index]);
		// Remove the arrow by index from the list.
		this.arrows.RemoveAt(index);
	}


}
