using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FaceCamera : MonoBehaviour {

    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update () {

        transform.rotation = camera.transform.rotation;
	}
}
