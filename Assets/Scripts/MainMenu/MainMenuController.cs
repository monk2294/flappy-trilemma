using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnNewGame()
    {
        StartCoroutine(LoadGameplayScene());
    }

    public void OnShowRecords()
    {

    }

    public void OnExit()
    {
        Application.Quit();
    }

    IEnumerator LoadGameplayScene()
    {
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync("Gameplay");

        while (!asyncSceneLoad.isDone)
        {
            yield return null;
        }
    }
}
