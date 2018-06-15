using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public GameObject throwingAxeWeapon;
	private Weapon throwingAxeWeaponScript;
    public GameObject laserWeapon;
	private Weapon laserWeaponScript;

	public bool explosive;

	public bool Explosive
	{
		get
		{
			return this.explosive;
		}
		set
		{
			this.explosive = value;
			throwingAxeWeaponScript.SetExplosive(value);
			laserWeaponScript.SetExplosive(value);
		}
	}

    public bool usingLaserWeapon;
    public bool UsingLaserWeapon
    { 
		get 
		{ 
			return this.usingLaserWeapon; 
		}
	 	set 
		{
			this.usingLaserWeapon = value;
			laserWeapon.SetActive(value);
			throwingAxeWeapon.SetActive(!value);
		} 
	}

	public float fireRateMultiplier;

	public float FireRateMultiplier
	{
		get
		{
			return fireRateMultiplier;
		}
		set
		{	
			this.fireRateMultiplier = value;
			laserWeaponScript.SetFireRateMultiplier(this.fireRateMultiplier);
			throwingAxeWeaponScript.SetFireRateMultiplier(this.fireRateMultiplier);
		}
	}

	
    // Use this for initialization
    void Start()
    {
		this.laserWeaponScript = laserWeapon.GetComponent<Weapon>();
		this.throwingAxeWeaponScript = throwingAxeWeapon.GetComponent<Weapon>();
		this.UsingLaserWeapon = usingLaserWeapon;
		this.Explosive = explosive;
		this.FireRateMultiplier = fireRateMultiplier;
    }
}
