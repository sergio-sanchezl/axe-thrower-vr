using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedModifier : MonoBehaviour
{
    [SerializeField] private float slowTimeScale = 0.5f;
    [SerializeField] private float normalTimeScale = 1.0f;

    [SerializeField] private float timeScale;
    // Use this for initialization
    void Awake()
    {
      bool reducedSpeedMode = PlayerPrefs.GetInt("reduced_speed_mode", 0) == 1;
      Time.timeScale = (reducedSpeedMode) ? slowTimeScale : normalTimeScale;
    }
}
