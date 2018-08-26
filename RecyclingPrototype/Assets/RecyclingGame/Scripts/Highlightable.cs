using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{

    public Color originalColour;

    Renderer renderer;
    Throwable throwable;
    Bin bin;
    TextMesh UI;

    public Color textColour = Color.white;

    public string highlightText;

    public bool highlighted;

    private void Awake()
    {
        renderer = GetComponentInChildren<MeshFilter>().GetComponent<Renderer>();
        throwable = GetComponentInChildren<Throwable>();
        bin = GetComponentInChildren<Bin>();
        UI = GetComponentInChildren<TextMesh>();
    }

    // Use this for initialization
    void Start()
    {

        //grab the colour 1 frame after
        originalColour = renderer.material.color;

        if (UI != null)
        {
            UI.color = new Color(1, 1, 1, 0);
        }


        if (throwable != null)
        {
            highlightText = throwable.throwableData.name;
        }

        if (bin != null)
        {
            highlightText = bin.binData.name;
        }
    }

    public void Highlight(Color highLightColour)
    {
        if (highlightText != null)
        {
            // UI.PopUpText(2, highlightText);

            UI.color = textColour;
            UI.text = highlightText;
        }
        //screen
        //Color targetColour = Color.white - (Color.white - originalColour) * (Color.white - highLightColour);

        //normal
        Color targetColour = highLightColour;
        // renderer.material.color = Color.Lerp(renderer.material.color, targetColour, 0.2f);
        renderer.material.color = targetColour;
        highlighted = true;

        


    }

    private void Update()
    {
        Reset();
    }

    public void Reset()
    {
        if (highlightText != null)
        {


            UI.color = Color.Lerp(UI.color, new Color(1,1,1,0), 0.1f);
        }

        if (renderer.material.color != originalColour)
            renderer.material.color = Color.Lerp(renderer.material.color, originalColour, 0.1f);

        highlighted = false;
    }
}
