using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedModifier : MonoBehaviour
{

    [SerializeField] private float timeScale;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		Time.timeScale = timeScale;
    }
}
