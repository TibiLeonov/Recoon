using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnStartSwipeOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(-78, 14, 46), "easetype", "easeoutcubic", "time", 1.5f));
        Destroy(this, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
