using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ChangeScale(Vector3 newScale, float duration)
    {
        Debug.Log("ChangeScale called with " + newScale + " " + duration);
        float timeElapsed = 0;
        while(timeElapsed < duration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, (timeElapsed / duration));
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }

    bool scaledUp = false;

    Coroutine currentCoroutine;

    public void DoSomethingWeird()
    {
        if(this.currentCoroutine != null)
            StopCoroutine(this.currentCoroutine);

        if(!scaledUp)
        {
            this.currentCoroutine = StartCoroutine(ChangeScale(new Vector3(2, 2, 2), 1f));
        } else
        {
            this.currentCoroutine = StartCoroutine(ChangeScale(new Vector3(1, 1, 1), 1f));
        }

        scaledUp = !scaledUp;
        
    }
}
