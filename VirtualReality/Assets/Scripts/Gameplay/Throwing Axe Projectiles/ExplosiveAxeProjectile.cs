using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAxeProjectile : AxeProjectile
{

    [SerializeField] private Vector3 center;

    [SerializeField] private float explosionDistance;

    [SerializeField] private float radius = 3f;

	[SerializeField] private GameObject explosionEffectPrefab;
    // Use this for initialization
    void Start()
    {

    }

    override public void Update()
    {
        base.Update();
        if (MustExplode())
        {
            Explode();
        }
    }

    private bool MustExplode()
    {
        return Vector3.Distance(center, this.transform.position) >= explosionDistance;
    }

    private void Explode()
    {
        // instantiate the explosion effect which stays in the world and must be destroyed afterwards.
        // //////
		GameObject explosion = Instantiate(explosionEffectPrefab, this.transform.position, Quaternion.identity);
		explosion.transform.LookAt(center);
		explosion.GetComponent<ExplosionEffectHandler>().Radius = radius;
		Destroy(explosion, 1f);
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius, LayerMask.GetMask("Targets"));
        List<GameObject> alreadyDealtDamage = new List<GameObject>();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (!alreadyDealtDamage.Contains(hitColliders[i].gameObject))
            {
                alreadyDealtDamage.Add(hitColliders[i].gameObject);
                IDamageable targetScript = hitColliders[i].transform.GetComponentInParent<IDamageable>();
                targetScript.DealDamage(damage);
            }

        }

		Destroy(this.gameObject);

    }


}
