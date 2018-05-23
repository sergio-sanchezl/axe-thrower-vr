using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This script must be inside every panel in the canvas. Only one panel
    should be active.
 */
public class AccessibilityForUI : MonoBehaviour
{
    public TextToSpeech textToSpeech;
    public UnityEngine.Object highlightPrefab;
    public int currentIndex;
    public GameObject[] interactiveElements;
    public string[] textToBeRead;
    public GameObject[] highlights;

    private int lengthOfElements;

    // variable to check inside 'OnEnable' if the 'Start' script has been
    // executed.
    private bool hasStarted;

    private float timeLastPress = 0.0f;
    // time that the input should be press to count as long tap.
    public float timeDelayThreshold = 1.0f;
    // Use this for initialization
    void OnEnable()
    {
        Debug.Log("OnEnable called in AccesibilityForUI!");
        if (hasStarted)
        {
            GameObject highlightToEnable = this.highlights[currentIndex];
            highlightToEnable.SetActive(true);
            ReadCurrent();
        }
    }
    void Start()
    {
        Debug.Log("Start called in AccessibilityForUI!");
        this.GetInfoFromElements();
        hasStarted = true;
    }
    void OnDisable()
    {
        Debug.Log("On disable in accessibility ui");
        highlights[currentIndex].SetActive(false);
        currentIndex = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            timeLastPress = Time.time;
            // ShiftIndex(true);
        }

        if (Input.GetButtonUp("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {

            if (Time.time - timeLastPress > timeDelayThreshold)
            {
                // Long press.
                Debug.Log("Long press!");
                InteractWithCurrent();
            }
            else
            {
                // Short press.
                Debug.Log("Short press!");
                ShiftIndex(true);
            }

            timeLastPress = Time.time;
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
}
