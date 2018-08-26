using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnStartSwipeOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(-113, 14, 83), "easetype", "easeoutcubic", "time", 1.5f));
        Destroy(this, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
