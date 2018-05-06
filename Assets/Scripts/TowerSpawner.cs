using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour {

    public MovableObject tower;
    public float maxHeight = 3.26f;
    public float minHeight = -1.45f;
    public float spawnRate = 0.75f;

    private float timer = 0.0f;
    private float timerStep = 0.0f;

    // Use this for initialization
    void Start () {
        timerStep = 1.0f / spawnRate;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        if (timer >= timerStep)
        {
            timer = 0.0f;
            // Time to spawn tower
            Vector3 position = transform.position;
            position.y = Random.value * (maxHeight - minHeight) + minHeight;
            var newTower = Instantiate(tower, position, transform.rotation);
            newTower.OnBecomeInvisible += () => Destroy(newTower.gameObject);
        }
	}
}
