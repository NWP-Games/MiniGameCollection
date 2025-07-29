using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private bool gameGoing = false;
    [SerializeField] private GameObject restartButton;

    private void Start()
    {
        points = 0;
        lives = 3;
        scoreText.text = "Score: " + points;
        livesText.text = "Lives: " + lives;
    }

    private void Update()
    {
        if (!gameGoing) return;

        if (Input.GetKeyDown(KeyCode.Space))
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
        lockpick.IncreaseSpeed();
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
        gameGoing = false;
        lockpick.SetGameGoing(gameGoing);
        lockpick.gameObject.SetActive(false);
        restartButton.SetActive(true);

        GameObject[] allActiveGameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject gameObject in allActiveGameObjects)
        {
            if (gameObject.name.Contains("UnlockPoint")) gameObject.IsDestroyed();
        }
    }

    public void StartGame()
    {
        gameGoing = true;
        lockpick.gameObject.SetActive(true);
        lockpick.gameObject.transform.rotation = new Quaternion (0, 0, 0, 0);
        lockpick.gameObject.transform.position = new Vector3(0, 2.75f, 0);
        lockpick.SetGameGoing(gameGoing);
        Vector3 spawnLocation = new Vector3(0, 2.75f, 0);
        Instantiate(unlockPoint, spawnLocation, Quaternion.identity);
        spawnLocation = new Vector3(2.75f, 0, 0);
        Instantiate(unlockPoint, spawnLocation, Quaternion.identity);
        spawnLocation = new Vector3(-2.75f, 0, 0);
        Instantiate(unlockPoint, spawnLocation, Quaternion.identity);
        spawnLocation = new Vector3(0, -2.75f, 0);
        Instantiate(unlockPoint, spawnLocation, Quaternion.identity);
        lives = 3;
        points = 0;
    }
}
