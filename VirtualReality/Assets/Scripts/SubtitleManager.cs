using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubtitleManager : MonoBehaviour
{

    public bool subtitlesEnabled;
    public Transform panel;
    public UnityEngine.Object subtitleLinePrefab;

    void Start()
    {
        this.subtitlesEnabled = PlayerPrefs.GetInt("subtitles", 0) == 0 ? false : true;
    }

    public void DisplaySubtitle(string subtitle, float time)
    {
        if (subtitlesEnabled)
        {
            // We instantiate the gameobject that will contain the text.
            GameObject subtitleGameObject = Instantiate(subtitleLinePrefab) as GameObject;
            // Set it as parent of the panel
            subtitleGameObject.transform.SetParent(panel, false);
            // Assign the text
            subtitleGameObject.GetComponent<Text>().text = subtitle;
            // Launch coroutine to delete it after a while.
            StartCoroutine(DeleteAfterSeconds(subtitleGameObject, time, 0.25f));
        }
    }

    IEnumerator DeleteAfterSeconds(GameObject gameObject, float timeToDestroy, float animationTime)
    {
        // We wait for the amount of time specified.
        yield return new WaitForSecondsRealtime(timeToDestroy);
        // After that, we get the current scale and animate it to
        // 0 during the duration of animationTime.
        Vector3 initialScale = gameObject.transform.localScale;
        Vector3 stepScale = gameObject.transform.localScale;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / animationTime;
            stepScale.y = Mathf.Lerp(initialScale.y, 0f, t);
            gameObject.transform.localScale = stepScale;
            yield return null;

        }
        // We finally destroy the GameObject.
        Destroy(gameObject);
        yield return null;
    }

    public static void ToggleSubtitles() {
        // We invert the value contained in PlayerPrefs.
        PlayerPrefs.SetInt("subtitles", PlayerPrefs.GetInt("subtitles", 0) == 0 ? 1 : 0);
    }
}
