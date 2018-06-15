using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{

    public ScoreManager scoreManager;
    public TimerManager timerManager;

    public WeaponManager weapon;
    Coroutine doubleFireRateCoroutine;
    Coroutine doublePointsCoroutine;
    Coroutine explosiveBuffCoroutine;
    Coroutine laserBuffCoroutine;

    // in seconds!
    float betterFireRateDuration = 15f;
    float doublePointsDuration = 15f;

    float explosiveBuffDuration = 15f;
    float laserBuffDuration = 15f;
    int extraSeconds = 30;
    int extraPoints = 15;

    public void ExecuteRandomBonus()
    {
        // 1 inclusive, 7 exclusive: this will yield 1, 2, 3, 4, 5 or 6.
        int randomNumber = Random.Range(1, 7);
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
                this.doubleFireRateCoroutine = StartCoroutine(BetterFireRateCoroutine());
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
            case 5:
                Debug.Log("555 - Explosive weapon!");
                // try to stop the coroutine (if it is null, don't try, that'll be a null exception).
                if (this.explosiveBuffCoroutine != null)
                {
                    StopCoroutine(this.explosiveBuffCoroutine);
                }
                // save coroutine reference and execute it.
                this.explosiveBuffCoroutine = StartCoroutine(ExplosiveWeaponBuffCoroutine());
                break;
            case 6:
                Debug.Log("666 - Laser weapon!");
                // try to stop the coroutine (if it is null, don't try, that'll be a null exception).
                if (this.laserBuffCoroutine != null)
                {
                    StopCoroutine(this.laserBuffCoroutine);
                }
                // save coroutine reference and execute it.
                this.laserBuffCoroutine = StartCoroutine(LaserWeaponBuffCoroutine());
                break;
            default:
                // do nothing.
                break;
        }
    }

    void HideDoubleFireRateTimer()
    {

    }

    void HideDoublePointsTimer()
    {

    }

    void HideExplosivesTimer()
    {

    }

    void HideLaserTimer()
    {

    }

    void StopDoubleFireRateCoroutine()
    {

    }

    void StopDoublePointsCoroutine()
    {

    }

    void StopExplosiveWeaponBuffCoroutine()
    {

    }

    void StopLaserWeaponBuffCoroutine()
    {

    }

    IEnumerator BetterFireRateCoroutine()
    {
        weapon.FireRateMultiplier = 4f;
        yield return new WaitForSeconds(this.betterFireRateDuration);
        weapon.FireRateMultiplier = 1f;
    }

    IEnumerator DoublePointsCoroutine()
    {
        scoreManager.SetMultiplier(2f);
        yield return new WaitForSeconds(this.doublePointsDuration);
        scoreManager.SetMultiplier(1f);
    }

    IEnumerator ExplosiveWeaponBuffCoroutine()
    {
        weapon.Explosive = true;
        yield return new WaitForSeconds(this.explosiveBuffDuration);
        weapon.Explosive = false;
    }

    IEnumerator LaserWeaponBuffCoroutine()
    {
        weapon.UsingLaserWeapon = true;
        yield return new WaitForSeconds(this.laserBuffDuration);
        weapon.UsingLaserWeapon = false;
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
