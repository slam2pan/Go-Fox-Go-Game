﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public Button gameOverButton;

    private float xRange = 5;
    private float gravityModifier = 2;
    private float speed = 10;
    private float jumpForce = 100;
    private bool isOnGround = true;
    public bool gameOver = false;
    public bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainPlayerMovement();
        MovePlayer();
        PlayerJump();
        /* SlowDown(); not yet */
    }

    // Moves the player horizontally if on the ground
    void MovePlayer()
    {
        if (gameOver == false)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        }
    }

    // Set boundaries for left right movemenet
    void ConstrainPlayerMovement()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }

    // Make the player jump
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }


    // Slow down the game after picking up powerup
    void SlowDown()
    {
        if (hasPowerup)
        {
            GetComponent<MoveTowardsPlayer>().setSpeed(3);
        } else
        {
            GetComponent<MoveTowardsPlayer>().setSpeed(5);
        }
    }

    // For collisions and to end the game
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Object"))
        {
            gameOver = true;
            gameOverButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
    }

}