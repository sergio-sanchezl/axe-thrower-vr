using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{

    public ScoreManager scoreManager;
    public TimerManager timerManager;

	public ThrowingAxeHand weapon;
    Coroutine doubleFireRateCoroutine;
    Coroutine doublePointsCoroutine;

    // in seconds!
    float doubleFireRateDuration = 15f;
    float doublePointsDuration = 15f;

    int extraSeconds = 30;
    int extraPoints = 15;
    public AnimationCurve cumulativeProbability;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void RandomizeBonus()
    {
        int randomNumber = Random.Range(1, 4);
        switch (randomNumber)
        {
            case 1:
                // try to stop the coroutine (if it is null, don't try, that'll be a null exception).
                if (this.doubleFireRateCoroutine != null)
                {
                    StopCoroutine(this.doubleFireRateCoroutine);
                }
                // save coroutine reference and execute it.
                this.doubleFireRateCoroutine = StartCoroutine(DoubleFireRateCoroutine());
                break;
            case 2:
                // try to stop the coroutine (if it is null, don't try, that'll be a null exception).
                if (this.doublePointsCoroutine != null)
                {
                    StopCoroutine(this.doublePointsCoroutine);
                }
                // save coroutine reference and execute it.
                this.doublePointsCoroutine = StartCoroutine(DoublePointsCoroutine());
                break;
            case 3:
                // 
				AddExtraPoints();
                break;
            case 4:
                // 
				AddExtraTime();
                break;
            default:
                // do nothing.
                break;
        }
    }

    IEnumerator DoubleFireRateCoroutine()
    {
		weapon.SetFireRateMultiplier(2f);
		yield return new WaitForSeconds(this.doubleFireRateDuration);
		weapon.SetFireRateMultiplier(1f);
    }

    IEnumerator DoublePointsCoroutine()
    {
		scoreManager.SetMultiplier(2f);
		yield return new WaitForSeconds(this.doubleFireRateDuration);
		scoreManager.SetMultiplier(1f);
    }

    void AddExtraPoints()
    {
        scoreManager.AddPoints(this.extraPoints);
    }

    void AddExtraTime()
    {
        timerManager.ExtendTime(this.extraSeconds);
    }
}
