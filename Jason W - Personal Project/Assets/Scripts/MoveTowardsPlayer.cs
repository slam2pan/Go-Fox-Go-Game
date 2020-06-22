using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoveTowardsPlayer : MonoBehaviour
{

    private PlayerController playerControllerScript;
    private GameManager gameManager;

    private float speed = 5;
    private float speedUpCond = 500;
    public float bottomBound = -12;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Constantly move object downwards towards the player unless the game is over
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }

        // Make the game harder as the score increases
        if (gameManager.getScore() > speedUpCond)
        {
            speedUpCond *= 2;
            speed += 2;
        }

        isOffScreen();
    }

    // Destroys object if it leaves the screen
    void isOffScreen()
    {
        if (transform.position.z < bottomBound && !gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    public void setSpeed(int setValue)
    {
        speed = setValue;
    }

    public float getSpeed()
    {
        return speed;
    }
}
