using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserWeapon : Weapon
{

    private LineRenderer lr;
    public GameObject bulletSpawner;
    public GameObject movingLight;

    public float explosionDistance;
    public Object explosionPrefab;
    [SerializeField] private GameObject fireParticleEmitter;
    void Start()
    {
        lr = bulletSpawner.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        SetExplosive(this.explosive);
    }

    override public void Update()
    {
        base.Update();
    }


    override public void SetExplosive(bool value)
    {
        base.SetExplosive(value);
        this.fireParticleEmitter.SetActive(value);
    }
    override protected void Shoot()
    {
        base.Shoot();
        bool somethingHit = false;
        Vector3 sourcePosition = bulletSpawner.transform.position;
        Vector3 hitPosition = Vector3.zero;

        if (explosive)
        {
            Vector3 direction = (bulletSpawner.transform.forward * 100) - bulletSpawner.transform.position;
            float magnitude = direction.magnitude;
            Vector3 normalizedDirection = direction / magnitude;
            Vector3 pointOfExplosion = (normalizedDirection * explosionDistance) + (Vector3.up * 1.5f);
            GameObject explosion = Instantiate(explosionPrefab, pointOfExplosion, Quaternion.identity) as GameObject;
            explosion.transform.LookAt(Vector3.zero);
            hitPosition = bulletSpawner.transform.position + (bulletSpawner.transform.forward * 100);

            explosion.GetComponent<ExplosionEffectHandler>().Radius = 3;
            Destroy(explosion, 1f);
            // deal damage in the radius.
            Collider[] hitColliders = Physics.OverlapSphere(pointOfExplosion, 3, LayerMask.GetMask("Targets"));
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

            LaunchAnimation(sourcePosition, hitPosition, somethingHit, 0.10f);
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(bulletSpawner.transform.position, bulletSpawner.transform.forward, out hit, 100, ~(1 << 8)))
            {
                Debug.Log("raycast hit something.");
                IDamageable hitEntity = hit.transform.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
                if (hitEntity == null)
                {
                    hitEntity = hit.transform.parent.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
                }
                if (hitEntity != null)
                {
                    hitEntity.DealDamage(this.damage);
                }
                somethingHit = true;
                hitPosition = hit.point;
            }
            else
            {
                Debug.Log("raycast didn't hit anything...");
                hitPosition = bulletSpawner.transform.position + (bulletSpawner.transform.forward * 100);
            }

            LaunchAnimation(sourcePosition, hitPosition, somethingHit, 0.10f);
        }

    }

    void LaunchAnimation(Vector3 sourcePosition, Vector3 targetPosition, bool hit, float laserDuration)
    {
        StartCoroutine(DisplayLaser(sourcePosition, targetPosition, hit, laserDuration));
    }

    IEnumerator DisplayLaser(Vector3 sourcePosition, Vector3 targetPosition, bool hit, float laserDuration)
    {
        lr.enabled = true;
        if (hit)
        {
            this.movingLight.SetActive(true);
            this.movingLight.transform.position = targetPosition;
        }

        lr.SetPosition(0, sourcePosition);
        lr.SetPosition(1, targetPosition);

        yield return new WaitForSeconds(laserDuration);

        lr.enabled = false;
        if (hit)
        {
            this.movingLight.SetActive(false);
        }
    }
    // Use this for initialization
    //void Start()
    //{
    //    lr = bulletSpawner.GetComponent<LineRenderer>();
    //    lr.positionCount = 2;
    //    lr.SetPosition(0, bulletSpawner.transform.position);
    //    lr.SetPosition(1, bulletSpawner.transform.position);
    //    lr.enabled = false;
    //    movingLight.SetActive(false);
    //}

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    Debug.Log("Straight Laser Script UPDATE!!!");
    //    lr.SetPosition(0, bulletSpawner.transform.position);
    //    lr.SetPosition(1, bulletSpawner.transform.position);
    //    Debug.Log("this transform forward." + bulletSpawner.transform.forward);
    //    Debug.Log("transform position" + bulletSpawner.transform.position);
    //    RaycastHit hit;
    //    if (Physics.Raycast(bulletSpawner.transform.position, bulletSpawner.transform.forward, out hit, 100, ~(1 << 8)))
    //    {
    //        movingLight.SetActive(true);
    //        lr.SetPosition(0, bulletSpawner.transform.position);
    //        lr.SetPosition(1, hit.point);

    //        // Vector that represents the direction of the actual line between the bullet spawner and the hit on the surface.
    //        Vector3 spawnToHitVectorDirection = Vector3.Normalize(hit.point - bulletSpawner.transform.position);

    //        movingLight.transform.position = hit.point - (spawnToHitVectorDirection * 0.15f);
    //        //movingLight.transform.rotation = bulletSpawner.transform.rotation;

    //        //Debug.DrawLine(bulletSpawner.transform.position, hit.point, Color.blue);

    //    }
    //    else
    //    {
    //        lr.SetPosition(0, bulletSpawner.transform.position);
    //        lr.SetPosition(1, bulletSpawner.transform.position + (bulletSpawner.transform.forward * 100));
    //        movingLight.SetActive(false);
    //        movingLight.transform.position = (bulletSpawner.transform.forward * 100);
    //    }
    //}

    //void Shoot()
    //{

    //}
}
