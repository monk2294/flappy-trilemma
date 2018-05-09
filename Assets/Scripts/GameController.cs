using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController current = null;

    public PlayerController player;
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Button retryButton;

    public UnityEngine.UI.Text recordsTitle;
    public UnityEngine.UI.Text recordsText;

    private int scores = 0;

    // Use this for initialization
    void Start () {
        current = this;
        hideDeathUI();
        OnAddScore(0);
        player.OnDeath += OnPlayerDead;
        player.OnAddScore += OnAddScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRestartEvent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitGameplay()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPlayerDead()
    {
        showDeathUI();
        TableOfRecords.OwnRecords().AddRecord(new GameRecord("Лудший чувак", scores));
        string result = "";
        foreach (var i in TableOfRecords.OwnRecords().Records)
        {
            result += i.PlayerName + ": " + i.Score + "\n";
        }
        recordsText.text = result;
    }

    public void OnAddScore(int score)
    {
        scores += score;
        scoreText.text = "Score: " + scores;
    }

    private void hideDeathUI()
    {
        retryButton.gameObject.SetActive(false);
        recordsTitle.gameObject.SetActive(false);
        recordsText.gameObject.SetActive(false);
    }

    private void showDeathUI()
    {
        retryButton.gameObject.SetActive(true);
        recordsTitle.gameObject.SetActive(true);
        recordsText.gameObject.SetActive(true);
    }
}
