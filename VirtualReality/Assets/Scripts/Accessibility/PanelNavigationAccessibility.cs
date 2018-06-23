using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This script must be inside every panel in the canvas. Only one panel
    should be active.
 */
public class PanelNavigationAccessibility : MonoBehaviour
{
    public TextToSpeech textToSpeech;
    public UnityEngine.Object highlightPrefab;
    public int currentIndex;
    public GameObject[] interactiveElements;
    public string[] textToBeRead;
    public GameObject[] highlights;

    public bool sweepModeActivated;
    public Coroutine sweepModeCoroutine;

    private int lengthOfElements;

    // variable to check inside 'OnEnable' if the 'Start' script has been
    // executed.
    private bool hasStarted;

    private float timeLastPress = 0.0f;
    // time that the input should be press to count as long tap.
    public float timeDelayThreshold = 0.5f;

    private float timeBetweenAutomaticFocusChange = 6.5f;
    // Use this for initialization
    void OnEnable()
    {
        // Debug.Log("OnEnable called in AccesibilityForUI!");
        if (hasStarted)
        {
            GameObject highlightToEnable = this.highlights[currentIndex];
            highlightToEnable.SetActive(true);
            ReadCurrent();
            SetSweepMode(this.sweepModeActivated);
        }
    }
    void Start()
    {
        // Debug.Log("Start called in AccessibilityForUI!");
        this.GetInfoFromElements();
        hasStarted = true;
        ReadCurrent();
        SetSweepMode(this.sweepModeActivated);
    }
    void OnDisable()
    {
        // Debug.Log("On disable in accessibility ui");
        highlights[currentIndex].SetActive(false);
        currentIndex = 0;
        StopSweepMode();
    }
    // Update is called once per frame
    void Update()
    {
        if (sweepModeActivated)
        {
            // if scan mode is activated, then just detect single presses, treating long presses as single.
            if (Input.GetButtonUp("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                InteractWithCurrent();
            }
        }
        else
        {
            // if scan mode is not activated, then we must differentiate between short press and long press.
            if (Input.GetButtonDown("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                timeLastPress = Time.unscaledTime;
                // ShiftIndex(true);
            }

            if (Input.GetButtonUp("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {

                if (Time.unscaledTime - timeLastPress > timeDelayThreshold)
                {
                    // Long press.
                    // Debug.Log("Long press!");
                    InteractWithCurrent();
                }
                else
                {
                    // Short press.
                    // Debug.Log("Short press!");
                    ShiftIndex(true);
                }

                timeLastPress = Time.unscaledTime;
            }
        }
    }

    public void SetSweepMode(bool value)
    {
        this.sweepModeActivated = value;
        if (this.gameObject.activeInHierarchy)
        {
            if (value)
            {
                StartSweepMode();
            }
            else
            {
                StopSweepMode();
            }
        }
    }
    public void StopSweepMode()
    {
        if (this.sweepModeCoroutine != null)
        {
            StopCoroutine(this.sweepModeCoroutine);
        }
    }
    public void StartSweepMode()
    {
        StopSweepMode();
        this.sweepModeCoroutine = StartCoroutine(SweepModeCoroutine());
    }
    public IEnumerator SweepModeCoroutine()
    {
        yield return new WaitForSecondsRealtime(timeBetweenAutomaticFocusChange);
        while (true)
        {
            ShiftIndex(true);
            yield return new WaitForSecondsRealtime(timeBetweenAutomaticFocusChange);
        }
    }

    // We obtain automatically the text to be read when interacting with each element.
    public void GetInfoFromElements()
    {
        this.lengthOfElements = interactiveElements.Length;
        this.textToBeRead = new string[lengthOfElements];
        this.highlights = new GameObject[lengthOfElements];
        int count = 0;
        foreach (GameObject item in interactiveElements)
        {
            GameObject highlightGameObject = Instantiate(highlightPrefab) as GameObject;
            // we can't use parent = item.transform directly, we must use the
            // SetParent method with 'false'. If not, the scale, position and rotation of the
            // rect of the highlight screws up and is placed in the center of the world.
            highlightGameObject.transform.SetParent(item.transform, false);
            highlightGameObject.transform.SetAsFirstSibling();
            highlightGameObject.SetActive(false);

            highlights[count] = highlightGameObject;
            // object obj;
            AlternativeText altText = item.GetComponent<AlternativeText>();
            if (altText != null)
            {
                this.textToBeRead[count] = altText.altText;
                altText.panel = this;
                altText.index = count;
            }
            else
            {
                Text text;
                switch (item.tag)
                {
                    case "Label":
                        text = item.transform.Find("Text").GetComponent<Text>();
                        this.textToBeRead[count] = "Etiqueta: " + text.text;
                        break;
                    case "Button":
                        text = item.transform.Find("Text").GetComponent<Text>();
                        this.textToBeRead[count] = "Botón: " + text.text;
                        break;
                }
            }
            count++;
        }

        GameObject highlightToEnable = this.highlights[currentIndex];
        highlightToEnable.SetActive(true);
    }

    public void ChangeTextToBeReadByIndex(int index, string text)
    {
        this.textToBeRead[index] = text;
    }

    public void ShiftIndex(bool forward)
    {
        GameObject highlightToDisable = this.highlights[currentIndex];
        highlightToDisable.SetActive(false);

        int shiftStep = forward ? 1 : -1;
        currentIndex = (currentIndex + shiftStep) % lengthOfElements;

        GameObject highlightToEnable = this.highlights[currentIndex];
        highlightToEnable.SetActive(true);
        ReadCurrent();
    }

    public void ReadCurrent()
    {
        Debug.Log("Currently reading: " + this.textToBeRead[currentIndex]);
        textToSpeech.Speak(this.textToBeRead[currentIndex]);
    }

    public void InteractWithCurrent()
    {
        Debug.Log("Interacting...");
        GameObject current = this.interactiveElements[currentIndex];
        // object obj;
        switch (current.tag)
        {
            case "Label":
                ReadCurrent();
                break;
            case "Button":
                Button button = current.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                }
                break;
        }

    }

    public void ChangeLongPressDuration(float duration)
    {
        this.timeDelayThreshold = duration;
    }
}
