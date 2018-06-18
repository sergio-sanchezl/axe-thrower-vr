using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassArrow : MonoBehaviour
{

    // Target of the arrow, i.e. to which gameobject will this
    // arrow point at.
    private GameObject target;

    // The gameobject containing the arrow's model.
    [SerializeField] private GameObject arrow;
    private Color arrowColor = Color.white;

    private Material material;
    // Use this for initialization
    void Start()
    {
        this.material = this.arrow.GetComponent<Renderer>().material;
        Colorizer colorizer = target.GetComponent<Colorizer>();
        if (colorizer != null)
        {
            this.arrowColor = colorizer.GetPrimaryColor();
        }
        this.material.SetColor("_Color", arrowColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {   
            // Vector3 targetPosition = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
            // this.transform.LookAt(targetPosition, this.transform.parent.up);
            this.transform.LookAt(target.transform);
            this.transform.localEulerAngles = new Vector3(0f,this.transform.localEulerAngles.y,0f);
        }

    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }


}
