using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxeHand : MonoBehaviour
{

    // Timestamp of last shot.
    private float previouslyFiredTime = 0;

    // Minimum delay between shots.
    public float fireRate;

    public float damage; // Damage dealt to the hit entity.
                         // Use this for initialization

    public GameObject axeInHand;
    public Object throwingAxePrefab;

    private Vector3 initialAxePosition;
    private Vector3 backAxePosition;

    public float animationTime = 0.12f;
    void Start()
    {
        this.initialAxePosition = this.axeInHand.transform.localPosition;
        this.backAxePosition = this.initialAxePosition + Vector3.back;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || (Input.touchCount > 0))
        {
            if (CanShoot())
            {
                Debug.Log("Can shoot!");
                Shoot();
            }
            else
            {
                Debug.Log("Cannot shoot yet...");
            }
        }
    }

    bool CanShoot()
    {
        float currentTime = Time.time;
        Debug.Log("PreviouslyFiredTime: " + previouslyFiredTime);
        Debug.Log("CurrentTime: " + currentTime);
        if (Mathf.Abs(currentTime - previouslyFiredTime) >= fireRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Shoot()
    {
        float currentTime = Time.time;
        this.previouslyFiredTime = currentTime;
        HideAxe();
        StartCoroutine(DelayCodeExecution(fireRate, (int i) => {
            ShowAxe();
        }));
        Debug.Log("Shooooootiiiing!!!!");

        GameObject throwingAxe = Instantiate(this.throwingAxePrefab) as GameObject;
        throwingAxe.transform.SetPositionAndRotation(this.axeInHand.transform.position, Camera.main.transform.rotation);
        // throwingAxe.transform.Rota
    }

    void HideAxe()
    {
        Debug.Log("HideAxe() called");
        StartCoroutine(MoveAxe(this.backAxePosition, this.animationTime));
    }

    void ShowAxe()
    {
        Debug.Log("ShowAxe() called");
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

    IEnumerator DelayCodeExecution(float time, System.Action<int> callBack) {
        Debug.Log("DelayCodeExecution before waiting.");
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("DelayCodeExecution after waiting.");
        callBack(1);
        yield return null;
    }
}
