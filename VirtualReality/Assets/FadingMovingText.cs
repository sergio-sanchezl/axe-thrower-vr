using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadingMovingText : MonoBehaviour {

	private RectTransform rectTransform;
	private Text text;
	private Outline outline;
	[SerializeField] private float secondsToFade = 2f;
	[SerializeField] private float finalTopRectValue;

	public string value = "";
	public bool positive = true;
	
	[SerializeField] private Color positiveColor;
	[SerializeField] private Color negativeColor;
	// Use this for initialization
	void Start () {
		this.rectTransform = this.GetComponent<RectTransform>();	
		this.text = this.GetComponent<Text>();
		this.text.text = value;
		this.outline = this.GetComponent<Outline>();
		this.text.color = (positive) ? positiveColor : negativeColor;
		StartCoroutine(AnimateCoroutine());
	}

	IEnumerator AnimateCoroutine() {
		float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / secondsToFade;
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, Mathf.Lerp(0, finalTopRectValue, t));
			text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(2,0,t));
			outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, Mathf.Lerp(2,0,t));
            yield return null;
        }
		Destroy(this.gameObject);
		yield return null;
	}
	
}
