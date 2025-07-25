using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class PTLGameManager : MonoBehaviour
{
    [SerializeField] private PTLLockpick lockpick;
    [SerializeField] private GameObject unlockPoint;
    [SerializeField] private float radius = 2.75f;
    [SerializeField] private int points = 0;
    [SerializeField] private int lives = 3;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private bool gameOver = false;

    private void Start()
    {
        points = 0;
        lives = 3;
        scoreText.text = "Score: " + points;
        livesText.text = "Lives: " + lives;
    }

    private void Update()
    {
        if (!gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject currentUnlockPoint = lockpick.GetCurrentUnlockPoint();
            if (currentUnlockPoint != null)
            {
                Destroy(currentUnlockPoint);
                SpawnUnlockPoint();
                Score();
            }
            else
            {
                Damage();
            }
        }
    }

    private void SpawnUnlockPoint()
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        Vector3 spawnLocation = new Vector3(x, y, 0);
        Instantiate(unlockPoint, spawnLocation, Quaternion.identity);
    }

    private void Score()
    {
        points++;
        scoreText.text = "Score: " + points;
    }

    private void Damage()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        lockpick.gameObject.SetActive(false);
    }

    public bool GetGameOver() { return gameOver; }
}
