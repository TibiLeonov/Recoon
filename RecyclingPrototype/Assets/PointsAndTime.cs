using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointsAndTime : MonoBehaviour {

    public float roundTimeStart=60;
    public float timeBetweenRounds = 5;
    public int points;
    public float roundTimeLeft;
    public Text timeText;
    public Text pointsText;
    public PlayerInput[] players;
    public TrashSpawner trashSpawner;

	void Start () {
        //testing, remove
        StartGame();
	}

    
	

    IEnumerator Countdown(float _timeToCount, Text _text)
    {
        float timer = _timeToCount;

        
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            _text.text = ((int)timer).ToString();
          
            yield return null;
        }

        WinState();
        
        
                 
        
        yield return null;
    }

	
    public void WinState()
    {
       
        foreach (PlayerInput player in players)
        {
            player.canInput = false;
            player.transform.GetComponent<PlayerMovement>().movementInput = Vector3.zero;
        }
        trashSpawner.StopSpawning();
        //display score
        //wait for something to call start game
       

    }




    public void StartGame()
    {
        foreach (PlayerInput player in players)
        {
            player.canInput = true;
            
        }
       
        roundTimeLeft = roundTimeStart;
        points = 0;

        trashSpawner.StartSpawning();
        StartCoroutine(Countdown(roundTimeLeft, timeText));
    }


   

    public void AddPoints(int _pointsToAdd)
    {
        points += _pointsToAdd;
        pointsText.text = points.ToString();
    }
}
