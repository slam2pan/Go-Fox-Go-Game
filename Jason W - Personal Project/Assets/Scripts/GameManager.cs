using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject[] objects;
    public GameObject powerup;
    private PlayerController playerControllerScript;
    private MoveTowardsPlayer moveTowards;
    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;

    private float spawnRange = 5;
    private float spawnRateObject = 1;
    private float spawnRatePowerup = 8;
    private float zStart = 30;
    private int score = 0;
    private int scoreInc = 1;
    private bool hasGameStarted = false;

    public void StartGame()
    {
        hasGameStarted = true;
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        playerControllerScript = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        moveTowards = GameObject.Find("Ground").GetComponent<MoveTowardsPlayer>();
        StartCoroutine(SpawnObjects());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(KeepScore());
    }

    // Connected to the Restart Button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Randomize spawn location and create an object
    IEnumerator SpawnObjects()
    {
        while (playerControllerScript.gameOver == false)
        {
            yield return new WaitForSeconds(spawnRateObject);

            spawnRateObject = 8 / moveTowards.getSpeed();
            int index = Random.Range(0, objects.Length);
            Vector3 spawnLocation = new Vector3(Random.Range(-spawnRange, spawnRange), objects[index].transform.position.y, zStart);
            Instantiate(objects[index], spawnLocation, objects[index].transform.rotation);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (playerControllerScript.gameOver == false)
        {
            yield return new WaitForSeconds(spawnRatePowerup);

            // Randomize spawn location
            spawnRatePowerup = 64 / moveTowards.getSpeed();
            Vector3 spawnSpot = new Vector3(Random.Range(-spawnRange, spawnRange), powerup.transform.position.y, zStart);
            Instantiate(powerup, spawnSpot, powerup.transform.rotation);
        }
    }

    // Add score each second
    IEnumerator KeepScore()
    {
        while (playerControllerScript.gameOver == false)
        {

            yield return new WaitForSeconds(0.01f);
            score += scoreInc;

            scoreText.text = "Score: " + score;
        }
    }

    public int getScore()
    {
        return score;
    }

    public bool hasTheGameStarted()
    {
        return hasGameStarted;
    }
}
