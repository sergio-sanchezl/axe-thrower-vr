using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{

    [SerializeField] private Vector3 center;
    [SerializeField] public float speed;
    [SerializeField] public float startAngle;
    [SerializeField] public float endAngle;

    [SerializeField] public float secondsMoving;
    [SerializeField] private float totalAngle;

    [SerializeField] private float currentAngle;
    public Vector3 Center { get; set; }

    public float Speed { get; set; }
    public float SecondsMoving { get; set; }
    
    public float StartAngle { get; set; }
    public float EndAngle { get; set; }
    [SerializeField] private bool movingToTheRight = true;
    // Use this for initialization
    void Start()
    {
		// this.startAngle = 45f;
		// this.endAngle = 45f;
		// this.secondsMoving = 5;
        this.totalAngle = endAngle + startAngle;
		// this.speed = this.totalAngle / this.secondsMoving;
        this.currentAngle = startAngle;
    }

    // Update is called once per frame
    void Update()
    {
		if(Mathf.Approximately(currentAngle, totalAngle)) {
			movingToTheRight = false;
		}
		if(Mathf.Approximately(currentAngle, 0)) {
			movingToTheRight = true;
		}
        float angle;
        if (movingToTheRight)
        {
            // if this step is going to surpass the maximum angle, just rotate the needed degrees to reach the maximum angle.
            angle = ((currentAngle + (this.speed * Time.deltaTime)) > totalAngle) ? (totalAngle - currentAngle) : (this.speed * Time.deltaTime);
            this.transform.RotateAround(this.center, Vector3.up, angle);
        }
        else
        {
			// if this step is going to be below the minimum angle (which is always 0), just rotate the needed degrees to reach the maximum angle.
            angle = ((currentAngle + (-this.speed * Time.deltaTime)) < 0) ? (0 - currentAngle) : (-this.speed * Time.deltaTime);
            this.transform.RotateAround(this.center, Vector3.up, angle);

        }
        this.currentAngle += angle;

    }
}
