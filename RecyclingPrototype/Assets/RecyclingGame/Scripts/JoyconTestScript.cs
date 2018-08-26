using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconTestScript : MonoBehaviour {

    public int ControllerID;

    public Vector2 axes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Action1_" + ControllerID))
        {
            Debug.Log("Action 1");
        }

        if (Input.GetButtonDown("Action2_" + ControllerID))
        {
            Debug.Log("Action 2");
        }

        //Vertical axis is flipped for both controllers        
        axes = new Vector2(Input.GetAxisRaw("Horizontal_" + ControllerID), -Input.GetAxisRaw("Vertical_" + ControllerID));
	}
}
