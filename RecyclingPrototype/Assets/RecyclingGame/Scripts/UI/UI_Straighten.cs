using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Straighten : MonoBehaviour {

    Vector3 Offset;
    private void Awake()
    {
        // Offset = transform.position - transform.parent.position;
        Offset = Vector3.up * 3;
    }

    // Use this for initialization
    void Start () {
        
	}

    private void Update()
    {
        Straighten();
    }

    public void Straighten()
    {
        Vector3 targetPosition = transform.parent.position + Offset;
        transform.position = targetPosition;
    }
}
