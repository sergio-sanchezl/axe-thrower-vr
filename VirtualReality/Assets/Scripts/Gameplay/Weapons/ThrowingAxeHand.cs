﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeHand : Weapon
{

    public GameObject axeInHand;
    public Object throwingAxePrefab;
    public Object explosiveAxePrefab;

    private Vector3 initialAxePosition;
    private Vector3 backAxePosition;

    public float animationTime = 0.12f;

    public Coroutine showAxeDelayCoroutine;

    [SerializeField] private float projectileSpeed = 1f;
    // public bool ExplosiveAxe { get { return this.explosiveAxe; } set { this.explosiveAxe = value; fireAxeParticleEmitter.SetActive(value); } }
    [SerializeField] private GameObject fireAxeParticleEmitter;
    public float ProjectileSpeed { get; set; }

    private bool started;
    void Start()
    {
        this.initialAxePosition = this.axeInHand.transform.localPosition;
        this.backAxePosition = this.initialAxePosition + Vector3.back;
        SetExplosive(this.explosive);
        started = true;
    }

    override public void SetExplosive(bool value)
    {
        base.SetExplosive(value);
        fireAxeParticleEmitter.SetActive(value);
    }
    override protected void Shoot()
    {
        base.Shoot();
        HideAxe();
        this.showAxeDelayCoroutine = StartCoroutine(DelayCodeExecution(fireRate / this.fireRateMultiplier, (int i) =>
        {
            ShowAxe();
        }));

        GameObject throwingAxe = Instantiate((explosive) ? this.explosiveAxePrefab : this.throwingAxePrefab) as GameObject;
        throwingAxe.transform.SetPositionAndRotation(this.axeInHand.transform.position, Camera.main.transform.rotation);
        throwingAxe.GetComponent<AxeProjectile>().movementSpeed = this.projectileSpeed;
    }

    void OnEnable() {
        if(started) {
            ShowAxe();
        }
    }
    void HideAxe()
    {
        //Debug.Log("HideAxe() called");
        StartCoroutine(MoveAxe(this.backAxePosition, this.animationTime));
    }

    void ShowAxe()
    {
        //Debug.Log("ShowAxe() called");
        StartCoroutine(MoveAxe(this.initialAxePosition, this.animationTime));
    }

    IEnumerator MoveAxe(Vector3 position, float time)
    {
        // this should be this.axeInHand.transform.localPosition but i do like
        // the effect this bug gives. it's not a bug, it's a feature!
        Vector3 initialPosition = this.transform.localPosition;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            this.axeInHand.transform.localPosition = Vector3.Lerp(initialPosition, position, t);
            yield return null;
        }
        yield return null;
    }

    IEnumerator DelayCodeExecution(float time, System.Action<int> callBack)
    {
        //Debug.Log("DelayCodeExecution before waiting.");
        yield return new WaitForSeconds(time);
        //Debug.Log("DelayCodeExecution after waiting.");
        callBack(1);
        yield return null;
    }

    override public void SetFireRateMultiplier(float multiplier)
    {
        Debug.Log("Setting FireRateMultiplier to " + multiplier);
        // The multiplier before applying any changes.
        float previousMultiplier = this.fireRateMultiplier;
        // The new multiplier is applied.
        base.SetFireRateMultiplier(multiplier);
        // some checks here, to check if when changing the multiplier the 
        // weapon should be already seen, or recalculate the time to make it appear again.

        // The time when the multiplier changed (i.e. right now)
        float timeFireRateMultiplierChanged = Time.time;

        // When should the axe appear, maybe this happened in the past. in that case,
        // show the axe right now.
        float newTimeToShowAxe = this.previouslyFiredTime + (this.fireRate / multiplier);

        // When was the axe scheduled to reappear.
        float previousTimeToShowAxe = this.previouslyFiredTime + (this.fireRate / previousMultiplier);
        if (timeFireRateMultiplierChanged >= newTimeToShowAxe && timeFireRateMultiplierChanged < previousTimeToShowAxe)
        {
            // If we must show the axe but it hasn't been shown yet, then show it.
            // Cancel the coroutine and show the axe immediately.
            StopCoroutine(this.showAxeDelayCoroutine);
            ShowAxe();
        }
        else
        {
            if (timeFireRateMultiplierChanged < newTimeToShowAxe)
            {
                // If we don't have to show the axe yet but we have to reschedule the show time, do so.
                if (this.showAxeDelayCoroutine != null)
                {
                    StopCoroutine(this.showAxeDelayCoroutine);
                }
                // Schedule the ShowAxe at the proper time, taking into account
                // the time that the axe has been hidden and the new time.
                if (this.isActiveAndEnabled)
                {
                    this.showAxeDelayCoroutine = StartCoroutine(DelayCodeExecution(newTimeToShowAxe - timeFireRateMultiplierChanged, (int i) =>
                {
                    ShowAxe();
                }));
                }

            }
        }
    }
}
