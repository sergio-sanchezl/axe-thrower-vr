using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontScaler : MonoBehaviour
{

    public FontScaleManager fontScaleManager;
    public int baseFontSize;
    public Text text;

    public int currentSize;
    // Use this for initialization
    void Start()
    {
        // Debug.Log("Font scaler AWAKE()");
        this.text = GetComponent<Text>();
        if (this.baseFontSize == 0)
        {
            this.baseFontSize = text.fontSize;
        }

        Debug.Log("teeeexxxxt size: " + text.fontSize);
        if (this.fontScaleManager == null)
        {
            this.fontScaleManager = GameObject.FindGameObjectWithTag("FontScaleManager").GetComponent<FontScaleManager>();
        }
        if (this.fontScaleManager != null)
        {
            this.fontScaleManager.AddObserver(this);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Notify()
    {
        Debug.Log("Font scaler NOTIFY!" + this.fontScaleManager.GetScale());
        this.text.fontSize = Mathf.RoundToInt(this.fontScaleManager.GetScale() * baseFontSize);
        this.currentSize = this.text.fontSize;
    }

    // OnEnable is called before Start, so we have to nullcheck.
    void OnEnable()
    {
        // Debug.Log("On Enable Font Scaler");
        if (this.fontScaleManager != null && text != null)
        {
            if (this.baseFontSize == 0)
            {
                this.baseFontSize = text.fontSize;
            }
            this.text.fontSize = Mathf.RoundToInt(this.fontScaleManager.GetScale() * baseFontSize);
            this.currentSize = this.text.fontSize;
        }

    }
}
