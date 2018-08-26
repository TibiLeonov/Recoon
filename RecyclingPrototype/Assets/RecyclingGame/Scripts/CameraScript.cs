using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {





    public Transform playerTransform;

    public float playerDistances;
    public float maxZoomRatio;
    public float minZoomDistance;
    public float maxZoomDistance;

    public Vector3 defaultTransformOffset;
    public Vector3 defaultRotation;
    private float defaultDistance;

    [Range(0, 1)]
    public float asymptoticSmoothRate = 0.5f;
    private Vector3 targetTransform;
    private Vector3 targetRotation;
    public float targetDistance;


    private Vector3 smoothTransform;
    private Vector3 smoothRotation;




    //public List<Transform> objectives;

    //in 2d, probably want to keep translational to x and y and rotational to only z
    //in 3d, avoid translational and just do rotational
    public Vector3 translationalShakeMax;
    public Vector3 rotationalShakeMax;


    Vector3 shakenCameraTransform;
    Vector3 shakenCameraRotation;

    [Range(0, 1)]
    float trauma;
    public float traumaDepletionRate = 0.5f;
    public float perlinNoiseScrollSpeed = 5f;



    private void Start()
    {
        //modified: if the offset values and rotation values are 0,0,0, then use the current values in inspector

        if (defaultTransformOffset == Vector3.zero)
        {
            defaultTransformOffset = transform.position - playerTransform.position;
        }

            targetTransform = playerTransform.position + defaultTransformOffset;


        //same for rotation
        if (defaultRotation == Vector3.zero)
        {
            defaultRotation = transform.localEulerAngles;
        }

            targetRotation = defaultRotation;

        if (defaultDistance == 0)
        {
            defaultDistance = Vector3.Distance(playerTransform.position, targetTransform);
            targetDistance = defaultDistance;
        }

            

        transform.position = targetTransform.normalized * targetDistance;
        transform.rotation = Quaternion.Euler(targetRotation);

        smoothTransform = targetTransform.normalized * targetDistance;
        smoothRotation = targetRotation;


    }



    private void LateUpdate()
    {


        MoveCamera();


        if (trauma > 0)
        {
            DoCameraShake();
            DepleteTrauma(traumaDepletionRate);

           
        }


        Camera.main.transform.position = smoothTransform + shakenCameraTransform;
        Camera.main.transform.rotation = Quaternion.Euler(smoothRotation + shakenCameraRotation);

    }



    public void MoveCamera()
    {
        //update the camera distance based on the distance apart of the players. might fuck up if players are far apart on first frame
        float clampedPlayerDistance = Mathf.Clamp(playerDistances, minZoomDistance, maxZoomDistance);
        float trueTargetDistance = Mathf.Lerp(defaultDistance, defaultDistance * maxZoomRatio, (clampedPlayerDistance - minZoomDistance) / maxZoomDistance);
        //lerp towards the true target distance, so that the distance changing isn't so rigid
        targetDistance = Mathf.Lerp(targetDistance, trueTargetDistance, 0.1f);


        targetTransform = playerTransform.position + defaultTransformOffset.normalized * targetDistance;
        targetRotation = defaultRotation;

        //change the value of smoothrate depending on using ability, slower movement while doing the charge
        if (Vector3.Distance(targetTransform, transform.position) > 0.01f)
        {
            smoothTransform += (targetTransform.normalized * targetDistance - smoothTransform) * asymptoticSmoothRate * Time.deltaTime * 30;
        }


        if (Vector3.Distance(targetRotation, transform.rotation.eulerAngles) > 0.01f)
        {
            smoothRotation += (targetRotation - smoothRotation) * asymptoticSmoothRate * Time.deltaTime * 30;
        }


    }

    //called from the playerAveragePositions gameobject/script
    public void GetPlayerDistance(float distance)
    {
        playerDistances = distance;
    }


    /// <summary>
    /// Add 0.2 or 0.5 for shake levels, keep in mind 1 is maximum total
    /// </summary>
    /// <param name="traumaToAdd"></param>
    public void AddTrauma(float traumaToAdd)
    {
        trauma = Mathf.Clamp01(trauma + traumaToAdd);

    }


    /// <summary>
    /// shakes camera according to the trauma variable
    /// </summary>
    public void DoCameraShake()
    {

        float xShake = trauma * trauma * translationalShakeMax.x * (Mathf.PerlinNoise(5.5f, Time.time * perlinNoiseScrollSpeed) * 2 - 1);
        float yShake = trauma * trauma * translationalShakeMax.y * (Mathf.PerlinNoise(6.5f, Time.time * perlinNoiseScrollSpeed) * 2 - 1);
        float zShake = trauma * trauma * translationalShakeMax.z * (Mathf.PerlinNoise(7.5f, Time.time * perlinNoiseScrollSpeed) * 2 - 1);

        float xShakerot = trauma * trauma * rotationalShakeMax.x * (Mathf.PerlinNoise(1.5f, Time.time * perlinNoiseScrollSpeed) * 2 - 1);
        float yShakerot = trauma * trauma * rotationalShakeMax.y * (Mathf.PerlinNoise(2.5f, Time.time * perlinNoiseScrollSpeed) * 2 - 1);
        float zShakerot = trauma * trauma * rotationalShakeMax.z * (Mathf.PerlinNoise(3.5f, Time.time * perlinNoiseScrollSpeed) * 2 - 1);

        shakenCameraTransform = new Vector3(xShake, yShake, zShake);
        shakenCameraRotation = new Vector3(xShakerot, yShakerot, zShakerot);


    }

    /// <summary>
    /// depletes trauma over time 
    /// </summary>
    /// <param name="thing"></param>
    public void DepleteTrauma(float traumaDepletionRate)
    {
        trauma = Mathf.Max(trauma - (traumaDepletionRate * Time.deltaTime), 0);

    }
}


