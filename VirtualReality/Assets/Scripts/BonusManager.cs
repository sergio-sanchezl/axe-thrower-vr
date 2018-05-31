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

    public void ExecuteRandomBonus()
    {
        // 1 inclusive, 5 exclusive: this will yield 1, 2, 3 or 4.
        int randomNumber = Random.Range(1, 5);
        switch (randomNumber)
        {
            case 1:
                Debug.Log("111 - Double fire rate!");
                // try to stop the coroutine (if it is null, don't try, that'll be a null exception).
                if (this.doubleFireRateCoroutine != null)
                {
                    StopCoroutine(this.doubleFireRateCoroutine);
                }
                // save coroutine reference and execute it.
                this.doubleFireRateCoroutine = StartCoroutine(DoubleFireRateCoroutine());
                break;
            case 2:
                Debug.Log("222 - Double points gain!");
                // try to stop the coroutine (if it is null, don't try, that'll be a null exception).
                if (this.doublePointsCoroutine != null)
                {
                    StopCoroutine(this.doublePointsCoroutine);
                }
                // save coroutine reference and execute it.
                this.doublePointsCoroutine = StartCoroutine(DoublePointsCoroutine());
                break;
            case 3:
                Debug.Log("333 - Extra points!");
                // extra points bonus.
				AddExtraPoints();
                break;
            case 4:
                Debug.Log("444 - Extra time!");
                // extra time bonus.
				AddExtraTime();
                break;
            default:
                // do nothing.
                break;
        }
    }

    IEnumerator DoubleFireRateCoroutine()
    {
		weapon.SetFireRateMultiplier(4f);
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
