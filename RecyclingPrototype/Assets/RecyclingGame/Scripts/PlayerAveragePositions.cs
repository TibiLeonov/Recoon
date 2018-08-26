using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAveragePositions : MonoBehaviour {

    public Transform[] players;

    Vector3 averagePosition;
    CameraScript cameraScript;


    public float defaultLerpRate = 0.9f;

    private void Awake()
    {
        cameraScript = Camera.main.GetComponentInChildren<CameraScript>();
    }

    // Use this for initialization
    void Start () {
		foreach (Transform player in players)
        {
            if (player == null)
            {
                Debug.Log("cannot have null players");
            }
        }
        MoveToAveragePosition(1);
	}

    void CalculateAveragePosition()
    {
        Vector3 positionSum = Vector3.zero;

        foreach(Transform player in players)
        {
            positionSum += player.position;
        }

        averagePosition = positionSum / players.Length;
    }

    void MoveToAveragePosition(float lerpRate = 0.1f)
    {
        CalculateAveragePosition();
        transform.position = Vector3.Lerp(transform.position, averagePosition, lerpRate);
    }

    float GiveCameraDistance()
    {
        //only works with 2 player as of yet
        float playerDistance = Vector3.Distance(players[0].position, players[1].position);
        return playerDistance;
    }

    private void Update()
    {
        if (cameraScript != null)
        {
            cameraScript.GetPlayerDistance(GiveCameraDistance());
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        MoveToAveragePosition();

    }
}
