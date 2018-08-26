using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_EndCard : MonoBehaviour {

    bool activated;
    public GameObject whiteWipe;

    public GameObject button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (activated)
        {
            if (Input.GetButtonDown("Action1_0") || Input.GetButtonDown("Action1_1") || Input.anyKeyDown)
            {
                iTween.MoveTo(whiteWipe, iTween.Hash("position", new Vector3(-2, 14, -28), "easetype", "easeincubic", "time", 1.8f, "oncomplete", "LoadScene", "oncompletetarget", gameObject));
                iTween.MoveTo(gameObject, iTween.Hash("position", transform.position + Vector3.right*3000, "easetype", "easeincubic", "time", 1.8f));
                button.GetComponent<Animator>().Play("press2");
            }
        }
    }

    public void Activate()
    {
        activated = true;
    }

    void LoadScene()
    {
        Debug.Log("ran");
        SceneManager.LoadScene(1);
    }
}
