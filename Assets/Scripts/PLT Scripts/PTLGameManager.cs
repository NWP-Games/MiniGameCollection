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
    [SerializeField] private GameObject pauseScreen;

    private void Start()
    {
        points = 0;
        lives = 3;
        scoreText.text = "Score: " + points;
        livesText.text = "Lives: " + lives;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(gameGoing) PauseGame();
            else UnpauseGame();
        }

        if (!gameGoing) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject currentUnlockPoint = lockpick.GetCurrentUnlockPoint();
            if (CheckLockpickPosition())
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

    private bool CheckLockpickPosition()
    {
        CapsuleCollider2D capsuleCollider = lockpick.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider != null)
        {
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            CapsuleCollider2D[] results = new CapsuleCollider2D[1]; // Array to store results

            if (capsuleCollider.OverlapCollider(filter, results) > 0)
            {
                return true;
            }
        }

        return false;
    }

    public void SpawnUnlockPoint()
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        Vector3 spawnLocation = new Vector3(x, y, 0);
        GameObject newUnlockPoint = Instantiate(unlockPoint, spawnLocation, Quaternion.identity);

        CircleCollider2D circleCollider = newUnlockPoint.GetComponent<CircleCollider2D>();
        if(circleCollider != null)
        {
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            CircleCollider2D[] results = new CircleCollider2D[1]; // Array to store results

            if (circleCollider.OverlapCollider(filter, results) > 0)
            {
                Destroy(newUnlockPoint);
                SpawnUnlockPoint();
            }
        }
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
            if (gameObject.name.Contains("Unlock"))
            {
                Destroy(gameObject);
            }
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
        lockpick.ResetSpeed();
    }

    public void PauseGame()
    {
        gameGoing = false;
        lockpick.SetGameGoing(gameGoing);
        pauseScreen.SetActive(true);
    }

    public void UnpauseGame()
    {
        pauseScreen.SetActive(false);
        if(lives > 0)
        {
            gameGoing = true;
            lockpick.SetGameGoing(gameGoing);
        }
    }
}
