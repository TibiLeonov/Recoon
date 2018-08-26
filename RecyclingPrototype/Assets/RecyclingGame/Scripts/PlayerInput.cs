using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int ControllerID;

    PlayerMovement pm;

    public bool canInput;

    private void Start()
    {
        pm = GetComponentInChildren<PlayerMovement>();
    }



    private void Update()
    {
        if (canInput)
        {
            if (Input.GetButtonDown("Action1_" + ControllerID) || Input.GetKeyDown("n"))
            {
                PlayerThrow();
            }
            if (Input.GetButtonDown("Action2_" + ControllerID) || Input.GetKeyDown("m"))
            {
                PlayerRoll();
            }



            PlayerMove();
        }

    }


    protected void PlayerThrow()
    {
        Debug.Log("fire");
        pm.DoAction();
    }

    protected void PlayerRoll()
    {
        Debug.Log("roll");
        pm.DoRoll();
    }

    protected void PlayerMove()
    {
        if (Input.GetAxisRaw("Horizontal_" + ControllerID) != 0 || Input.GetAxisRaw("Vertical_" + ControllerID) != 0)
        {
            //joycon vertical axes are flipped, so i changed it to negative
            pm.movementInput = new Vector3(Input.GetAxisRaw("Horizontal_" + ControllerID),0, -Input.GetAxisRaw("Vertical_" + ControllerID));
        }
        else
        {
            pm.movementInput = Vector3.zero;
        }
    }




}
