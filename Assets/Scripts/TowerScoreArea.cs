using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScoreArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerController = collision.gameObject.GetComponent<PlayerController>();
        Debug.Log(collision.gameObject.name);
        if (playerController != null)
        {
            Debug.Log("Player controller score hit");
            playerController.AddScore();
            Destroy(gameObject);
        }
    }
}
