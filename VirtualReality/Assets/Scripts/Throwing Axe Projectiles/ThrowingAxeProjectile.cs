using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeProjectile : AxeProjectile, IChildsCollisionReceiver
{

    // public float rotationSpeed = 1f;
    // public float movementSpeed = 1f;
    public bool stuckInPlace = false;

    // public Transform whatShouldRotate;
    // public float damage = 0f;

    private Coroutine destructionCoroutine;
    // Use this for initialization
    void Start()
    {
        this.destructionCoroutine = StartCoroutine((this.gameObject.AddComponent(typeof(DestructionScheduler)) as DestructionScheduler).DestroyAfterTime(3f));
    }

    // Update is called once per frame
    override public void Update()
    {
        if (!stuckInPlace)
        {
            base.Update();
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
        // now we are stuck in place, so we stop the schedule of the destruction of the axe.
        stuckInPlace = true;
        StopCoroutine(this.destructionCoroutine);
        this.transform.parent = collision.transform;
        // THIS CAN BE REPLACED WITH GetComponentInChildren!... okay, not really.
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
