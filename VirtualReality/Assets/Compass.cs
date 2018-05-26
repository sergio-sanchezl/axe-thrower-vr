using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

	public Object arrowPrefab;

	List<GameObject> targetsTracking;
	List<GameObject> arrows;
	void Start () {
		this.targetsTracking = new List<GameObject>();
		this.arrows = new List<GameObject>();
	}
	
	void Update () {
		
	}

	public void AddTargetToBeTracked(GameObject newTarget) {
		if (!this.targetsTracking.Contains(newTarget)) {
			this.targetsTracking.Add(newTarget);
			GameObject newArrow = Instantiate(arrowPrefab) as GameObject;
			newArrow.transform.localPosition = Vector3.zero;
			this.arrows.Add(newArrow);
		}
	}

	public void DeleteTarget(GameObject target) {
		this.targetsTracking.Remove(target);
	}


}
