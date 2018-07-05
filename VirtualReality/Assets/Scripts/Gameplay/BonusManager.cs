using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{

    public ScoreManager scoreManager;
    public TimerManager timerManager;

    public WeaponManager weapon;
    Coroutine quickReloadCoroutine;
    Coroutine doublePointsCoroutine;
    Coroutine explosiveBuffCoroutine;
    Coroutine laserBuffCoroutine;

    // in seconds!
    int quickReloadDuration = 15;
    int doublePointsDuration = 15;

    int explosiveBuffDuration = 15;
    int laserBuffDuration = 15;
    int extraSeconds = 30;
    int extraPoints = 15;

    [SerializeField] private AudioController pointsSound;
    [SerializeField] private AudioController extraTimeSound;
    [SerializeField] private AudioController quickReloadSound;
    [SerializeField] private AudioController doublePointsSound;
    [SerializeField] private AudioController explosiveBuffSound;
    [SerializeField] private AudioController laserBuffSound;

    int quickReloadRemainingSeconds = 0;
    int QuickReloadRemainingSeconds
    {
        get
        {
            return this.quickReloadRemainingSeconds;
        }
        set
        {
            this.quickReloadRemainingSeconds = value;
            quickReloadTimerText.text = ParseTime(this.quickReloadRemainingSeconds);
            if(this.quickReloadRemainingSeconds <= 0)
            {
                quickReloadTimerContainer.SetActive(false);
            }
            else
            {
                quickReloadTimerContainer.SetActive(true);
            }
        }
    }
    public Text quickReloadTimerText;
    public GameObject quickReloadTimerContainer;
    int doublePointsRemainingSeconds = 0;
    int DoublePointsRemainingSeconds
    {
        get
        {
            return this.doublePointsRemainingSeconds;
        }
        set
        {
            this.doublePointsRemainingSeconds = value;
            doublePointsTimerText.text = ParseTime(this.doublePointsRemainingSeconds);
            if(this.doublePointsRemainingSeconds <= 0)
            {
                doublePointsTimerContainer.SetActive(false);
            }
            else
            {
                doublePointsTimerContainer.SetActive(true);
            }
        }
    }
    public Text doublePointsTimerText;
    public GameObject doublePointsTimerContainer;
    int explosiveBuffRemainingSeconds = 0;
    int ExplosiveBuffRemainingSeconds
    {
        get
        {
            return this.explosiveBuffRemainingSeconds;
        }
        set
        {
            this.explosiveBuffRemainingSeconds = value;
            explosiveBuffTimerText.text = ParseTime(this.explosiveBuffRemainingSeconds);
            if(this.explosiveBuffRemainingSeconds <= 0)
            {
                explosiveBuffTimerContainer.SetActive(false);
            }
            else
            {
                explosiveBuffTimerContainer.SetActive(true);
            }
        }
    }
    public Text explosiveBuffTimerText;
    public GameObject explosiveBuffTimerContainer;
    int laserBuffRemainingSeconds = 0;
    int LaserBuffRemainingSeconds
    {
        get
        {
            return this.laserBuffRemainingSeconds;
        }
        set
        {
            this.laserBuffRemainingSeconds = value;
            laserBuffTimerText.text = ParseTime(this.laserBuffRemainingSeconds);
            if(this.laserBuffRemainingSeconds <= 0)
            {
                laserBuffTimerContainer.SetActive(false);
            }
            else
            {
                laserBuffTimerContainer.SetActive(true);
            }
        }
    }
    public Text laserBuffTimerText;
    public GameObject laserBuffTimerContainer;

    void Start() {
        this.LaserBuffRemainingSeconds = this.laserBuffRemainingSeconds;
        this.QuickReloadRemainingSeconds = this.quickReloadRemainingSeconds;
        this.DoublePointsRemainingSeconds = this.doublePointsRemainingSeconds;
        this.ExplosiveBuffRemainingSeconds = this.explosiveBuffRemainingSeconds;
    }
    public void ExecuteRandomBonus()
    {
        // 1 inclusive, 8 exclusive: this will yield 1, 2, 3, 4, 5, 6 or 7.
        int randomNumber = UnityEngine.Random.Range(1, 8);
        switch (randomNumber)
        {
            case 1:
                Debug.Log("111 - Double fire rate!");
                // try to stop the coroutine (if it is null, don't try, that'll be a null exception).
                if (this.quickReloadCoroutine != null)
                {
                    StopCoroutine(this.quickReloadCoroutine);
                }
                // save coroutine reference and execute it.
                this.quickReloadCoroutine = StartCoroutine(QuickReloadCoroutine());
                quickReloadSound.Play();
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
                doublePointsSound.Play();
                break;
            case 3:
                Debug.Log("333 - Extra points!");
                // extra points bonus.
                AddExtraPoints();
                pointsSound.Play();
                break;
            case 4:
            case 7:
                Debug.Log("444 - Extra time!");
                // extra time bonus.
                AddExtraTime();
                extraTimeSound.Play();
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
                explosiveBuffSound.Play();
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
                laserBuffSound.Play();
                break;
            default:
                // do nothing.
                break;
        }
    }

    IEnumerator QuickReloadCoroutine()
    {
        weapon.FireRateMultiplier = 4f;
        this.QuickReloadRemainingSeconds = this.quickReloadDuration;// quickReload
        while(this.QuickReloadRemainingSeconds > 0) {
            yield return new WaitForSeconds(1f);
            this.QuickReloadRemainingSeconds = this.QuickReloadRemainingSeconds - 1;
        }
        weapon.FireRateMultiplier = 1f;
    }

    IEnumerator DoublePointsCoroutine()
    {
        scoreManager.SetMultiplier(2f);
        this.DoublePointsRemainingSeconds = this.doublePointsDuration;// doublePoints
        while(this.DoublePointsRemainingSeconds > 0) {
            yield return new WaitForSeconds(1f);
            this.DoublePointsRemainingSeconds = this.DoublePointsRemainingSeconds - 1;
        }
        scoreManager.SetMultiplier(1f);
    }

    IEnumerator ExplosiveWeaponBuffCoroutine()
    {
        weapon.Explosive = true;
        this.ExplosiveBuffRemainingSeconds = this.explosiveBuffDuration;// explosiveBuff
        while(this.ExplosiveBuffRemainingSeconds > 0) {
            yield return new WaitForSeconds(1f);
            this.ExplosiveBuffRemainingSeconds = this.ExplosiveBuffRemainingSeconds - 1;
        }
        weapon.Explosive = false;
    }

    IEnumerator LaserWeaponBuffCoroutine()
    {
        weapon.UsingLaserWeapon = true;
        this.LaserBuffRemainingSeconds = this.laserBuffDuration;// laserBuff
        while(this.LaserBuffRemainingSeconds > 0) {
            yield return new WaitForSeconds(1f);
            this.LaserBuffRemainingSeconds = this.LaserBuffRemainingSeconds - 1;
        }
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

    string ParseTime(int seconds) {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D1}:{1:D2}", time.Minutes, time.Seconds);
    }
}
