using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shadow : MonoBehaviour {

    public Color shadowColour = new Color(0.2f, 0.2f, 0.2f);

    TextMesh textMesh;
    TextMesh parentText;


	// Use this for initialization
	void Awake () {
        parentText = transform.parent.GetComponent<TextMesh>();
        textMesh = GetComponent<TextMesh>();


    }

    private void Start()
    {
        transform.localPosition = new Vector3(0.02f, -0.02f, 0.01f);
        textMesh.text = parentText.text;

        textMesh.color = shadowColour;
    }

    void Update () {
        //match parent text's opacity

        float alpha = parentText.color.a;

        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
        textMesh.text = parentText.text;
    }
}
