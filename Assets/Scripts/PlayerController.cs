﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public delegate void AddScoreAction(int score);
    public delegate void DeathAction();

    public event AddScoreAction OnAddScore;
    public event DeathAction OnDeath;

    public float jumpInitialSpeed = 10.0f;
    public bool godMode = false;

    private bool touched = false;
    private bool touchSupported = false;
    private Rigidbody2D rb;
    private ParticleSystem deathParticles;
    private bool isDead = false;

	// Use this for initialization
	void Start () {
        touchSupported = Input.touchSupported;
        rb = GetComponent<Rigidbody2D>();
        deathParticles = GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!isDead && isClicked())
        {
            // Compute jump initial speed
            float jumpVelocity = rb.velocity.y / 10 + jumpInitialSpeed;
            rb.velocity = new Vector2(0.0f, jumpVelocity);
        }
        updateOrientation();
	}

    public void AddScore()
    {
        if (OnAddScore != null)
        {
            OnAddScore(1);
        }
    }

    private bool isClicked()
    {
        if (touchSupported)
        {
            if (!touched && Input.touchCount > 0)
            {
                touched = true;
                return true;
            }
            if (touched && Input.touchCount == 0)
            {
                touched = false;
            }
            return false;
        }
        else
        {
            return Input.GetMouseButtonDown(0);
        }
    }

    private void updateOrientation()
    {
        float verticalSpeed = rb.velocity.y;
        Vector3 angles = transform.eulerAngles;
        angles.z = verticalSpeed * 2.0f;
        transform.eulerAngles = angles;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!godMode)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                OnGameOver();
            }
        }
    }

    private void OnGameOver()
    {
        if (!isDead)
        {
            deathParticles.Emit(5);
            gameObject.layer = 9; // Dead player doesn't collide with towers
            rb.velocity = new Vector2(0.0f, jumpInitialSpeed);
            isDead = true;
            if (OnDeath != null)
            {
                OnDeath();
            }
        }
    }
}
