using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedModifier : MonoBehaviour
{

    [SerializeField] private float timeScale;

    [SerializeField] private ThrowingAxeHand weapon;
    // Use this for initialization
    void Start()
    {
      // this.timeScale = PlayerPrefs.GetFloat("game_speed", 1.0f);
      // weapon.ProjectileSpeed = weapon.ProjectileSpeed / timeScale;
      Time.timeScale = timeScale;
    }
}
