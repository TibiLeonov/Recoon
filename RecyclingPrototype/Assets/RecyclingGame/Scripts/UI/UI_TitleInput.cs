using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_TitleInput : MonoBehaviour {

    GameObject WhiteSwiper;
    public GameObject button;
    AsyncOperation asyncLoad;

    // Use this for initialization
    void Awake() {
        WhiteSwiper = transform.GetChild(0).gameObject;
        
    }

    private void Start()
    {
        asyncLoad = SceneManager.LoadSceneAsync(1);
        StartCoroutine(LoadYourAsyncScene());
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Action1_0") || Input.GetButtonDown("Action1_1") || Input.anyKeyDown)
        {
            iTween.MoveTo(WhiteSwiper, iTween.Hash("position", new Vector3(-18, 0, 0), "delay", 1, " easetype", "easeincubic", "time", 1.8f, "oncomplete", "LoadScene", "oncompletetarget", gameObject));
            button.GetComponent<Animator>().Play("press");

        }

	}

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    void LoadScene()
    {
        asyncLoad.allowSceneActivation = true;
    }
}
