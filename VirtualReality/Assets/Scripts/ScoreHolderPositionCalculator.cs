using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolderPositionCalculator : MonoBehaviour {

	[SerializeField] private Vector3 furtherPoint;
	[SerializeField] private Vector3 closerPoint;

	private float maximumScale;
	private float minimumScale;
	[SerializeField] private FontScaleManager fontScaler;

	private float scale;

	// Use this for initialization
	void Start () {
		// If scale is less than 1, make it 1 still. We don't want the
		// canvas to be too far away.
		this.scale = this.fontScaler.scale >= 1 ? this.fontScaler.scale : 1;
		this.minimumScale = 1;
		this.maximumScale = this.fontScaler.maximumScale;
		this.transform.position = Vector3.Lerp(furtherPoint, closerPoint, (this.scale - minimumScale) / (maximumScale - minimumScale));
	}
}
