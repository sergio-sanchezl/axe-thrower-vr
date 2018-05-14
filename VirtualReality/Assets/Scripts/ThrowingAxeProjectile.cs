using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeProjectile : MonoBehaviour, IChildsCollisionReceiver
{

    public float rotationSpeed = 1f;
    public float movementSpeed = 1f;
    public bool stuckInPlace = false;

    public Transform whatShouldRotate;
    public float damage = 0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!stuckInPlace)
        {
            this.transform.Translate((Vector3.forward * movementSpeed * Time.deltaTime), Space.Self);
            whatShouldRotate.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
        }
    }
    public void ReceiveCollisionEnter(Collision collision)
    {
        // We dont want axes to collide with other axes.
        if (collision.transform.tag.Equals("Axe"))
        {
            return;
        }
        whatShouldRotate.GetComponent<BoxCollider>().enabled = false;
        Destroy(whatShouldRotate.GetComponent<Rigidbody>());
        Debug.Log("Collision detected!");
        // We rotate the axe according to the normal of the face of the point of collision.
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);
        stuckInPlace = true;
        this.transform.parent = collision.transform;
        IDamageable damageable = collision.transform.GetComponent(typeof(IDamageable)) as IDamageable;
        if (damageable == null && collision.transform.parent != null)
        {
            damageable = collision.transform.parent.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        }
        if (damageable != null)
        {
            damageable.DealDamage(this.damage);
        }
        
    }

    public void ReceiveCollisionExit(Collision collision)
    {
        // do nothing.
    }

    public void ReceiveCollisionStay(Collision collision)
    {
        // do nothing.
    }
}
