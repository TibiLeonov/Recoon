using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUp : MonoBehaviour {

   // SpriteRenderer[] icons;
    GameObject[] rotationalAxes;

    //repurposed for sprites

    //public  TextMesh text;
    public SpriteRenderer sprite;

    private void Awake()
    {
        // icons = GetComponentsInChildren<SpriteRenderer>();
        // text = GetComponentInChildren<TextMesh>();
        sprite = GetComponentInChildren<SpriteRenderer>();

       // rotationalAxes = new GameObject[icons.Length];
       //for (int i =0; i<icons.Length; i++)
       // {
       //     rotationalAxes[i] = icons[i].transform.parent.gameObject;
       // }
    }

    // Use this for initialization
    void Start () {

        HideAll();

    }

    /*
    public void PopUp(float duration, int iconIndex)
    {
        HideAll();

       // icons[iconIndex].gameObject.SetActive(true);
        Vector3 rotationAmount = new Vector3(0, 0, 35);
        Vector3 scaleAmount = new Vector3(1.2f, 1.2f, 1.2f);
        iTween.PunchScale(rotationalAxes[iconIndex].gameObject, iTween.Hash("amount", scaleAmount, "time", duration, "oncomplete", "HideAll", "oncompletetarget", gameObject));
      //  iTween.FadeTo(icons[iconIndex].gameObject, iTween.Hash("alpha", 0, "time", duration/2, "delay", duration/2));
    }
    */
    public void HideAll()
    {

        //iTween.Stop(gameObject, true);

        //    foreach (SpriteRenderer icon in icons)
        //   {
        //       icon.gameObject.SetActive(false);
        //       icon.material.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1);
        //   }

        // iTween.FadeTo(sprite.gameObject, 1, 0f);
        // sprite.gameObject.SetActive(false);

        iTween.FadeTo(sprite.gameObject, 0, 0f);
    }


    public void PopUpImage(float duration)
    {
        iTween.FadeTo(sprite.gameObject, 1, 0f);

       // sprite.gameObject.SetActive(true);
        //iTween.Stop(gameObject, true);

        Vector3 scaleAmount = new Vector3(transform.parent.localScale.x*0.2f, transform.parent.localScale.y * 0.2f, transform.parent.localScale.z * 0.2f);
        GameObject scaleAxis = sprite.transform.parent.gameObject;
        // Vector3 rotationAmount = new Vector3(0, 0, 15);
        // iTween.PunchRotation(scaleAxis.gameObject, iTween.Hash("amount", rotationAmount, "time", duration, "oncomplete", "HideAll", "oncompletetarget", gameObject));
        iTween.PunchScale(scaleAxis.gameObject, iTween.Hash("amount", scaleAmount, "time", duration, "oncomplete", "HideAll", "oncompletetarget", gameObject));
        iTween.FadeTo(sprite.gameObject, iTween.Hash("alpha", 0, "time", duration / 2, "delay", duration / 2));
    }

    void ResetTint()
    {
        iTween.FadeTo(sprite.gameObject, 1, 0f);
    }

}
