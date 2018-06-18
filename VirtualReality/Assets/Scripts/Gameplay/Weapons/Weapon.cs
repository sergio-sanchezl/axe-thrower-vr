using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    // Timestamp of last shot.
    protected float previouslyFiredTime = 0;

    // Minimum delay between shots.
    public float fireRate;

    public float damage; // Damage dealt to the hit entity.

    public bool explosive;

    public float fireRateMultiplier = 1f;
    void Start()
    {
		
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (Input.GetButtonDown("Fire1") || (Input.touchCount > 0))
        {
            if (CanShoot())
            {
                //Debug.Log("Can shoot!");
                Shoot();
            }
            else
            {
                //Debug.Log("Cannot shoot yet...");
            }
        }
    }

    protected bool CanShoot()
    {
        float currentTime = Time.time;
        if (Mathf.Abs(currentTime - previouslyFiredTime) >= fireRate / this.fireRateMultiplier)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    virtual protected void Shoot()
    {
        float currentTime = Time.time;
        this.previouslyFiredTime = currentTime;
    }

    virtual public void SetFireRateMultiplier(float multiplier)
    {
        this.fireRateMultiplier = multiplier;
    }

    virtual public void SetExplosive(bool value)
    {
        this.explosive = value;
    }
}
