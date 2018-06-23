using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    [SerializeField] private Renderer rendererToFetchMaterial;
    private Material materialToColorize;
    // Primary color of the target.
    [SerializeField] private Color primaryColor;

    // Secondary color of the target.
    [SerializeField] private Color secondaryColor;

    // Field to look at in PlayerPrefs to get the primary color for this target.
    [SerializeField] private string primaryColorField;

    // Field to look at in PlayerPrefs to get the secondary color for this target.
    [SerializeField] private string secondaryColorField;

    public Color PrimaryColor { get { return this.primaryColor; } set { this.primaryColor = value; if (this.materialToColorize != null) { this.materialToColorize.SetColor("_PrimaryColor", primaryColor); } } }
    public Color SecondaryColor { get { return this.secondaryColor; } set { this.secondaryColor = value; if (this.materialToColorize != null) { this.materialToColorize.SetColor("_SecondaryColor", secondaryColor); } } }
    // Use this for initialization
    void Start()
    {
        this.materialToColorize = this.rendererToFetchMaterial.material;
        // this.materialToColorize.SetColor("_PrimaryColor", primaryColor);
        this.materialToColorize.SetColor("_SecondaryColor", secondaryColor);
        LoadColors();
    }

    private void LoadColors()
    {
        this.PrimaryColor = GetColorFromPlayerPrefs(primaryColorField);
        // this.secondaryColor = GetColorFromPlayerPrefs(secondaryColorField);
    }

    private Color GetColorFromPlayerPrefs(string key)
    {
        string colorString;
        colorString = PlayerPrefs.GetString(key, "RGBA(1,1,1,1)");
        //Remove the header and brackets
        colorString = colorString.Replace("RGBA(", "");
        colorString = colorString.Replace(")", "");

        //Get the individual values (red green blue and alpha)
        var strings = colorString.Split(","[0]);

        Color outputColor;
        outputColor = Color.black; // initialize to 0 0 0 1.
        for (var i = 0; i < 4; i++)
        {
            outputColor[i] = System.Single.Parse(strings[i]);
        }
        return outputColor;
    }
    public Color GetPrimaryColor()
    {
        return this.primaryColor;
    }

    public Color GetSecondaryColor()
    {
        return this.secondaryColor;
    }

    public void SetPrimaryColor(Color color)
    {
        this.primaryColor = color;
    }

    public void SetSecondaryColor(Color color)
    {
        this.secondaryColor = color;
    }
}
