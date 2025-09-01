using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FBGameManager : MonoBehaviour
{
    [SerializeField] private FBBirdScript bird;
    [SerializeField] private FBPipeSpawner pipeSpawner;
    [SerializeField] private bool gameGoing = true;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject instructionScreen;
    [SerializeField] private GameObject menuButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameGoing) PauseGame();
            else UnpauseGame();
        }

        if (!gameGoing) return;
    }

    public void StartGame()
    {
        pipeSpawner.DestoryPipes();
        instructionScreen.SetActive(false);
        menuButton.SetActive(false);
        score = 0;
        bird.SetIsAlive(true);
        bird.SetIsGameGoing(false);
        bird.SetIsGameGoing(true);
        bird.transform.position = new Vector3(-2, 0, 0);
        bird.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameGoing = true;
        bird.SetIsGameGoing(gameGoing);
        pipeSpawner.SetIsGameGoing(true);
        pipeSpawner.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gameGoing = false;
        //bird.SetIsGameGoing(false);
        pipeSpawner.SetIsGameGoing(false);
        //pauseScreen.SetActive(true);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
    }

    public void Score() 
    { 
        score++; 
        scoreText.text = "Score: " + score;
    }

    private void ResetGame()
    {
        score = 0;
        bird.SetIsAlive(true);
        bird.transform.position = new Vector3 (-2, 0, 0);
        bird.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void PauseGame()
    {
        gameGoing = false;
        bird.SetIsGameGoing(gameGoing);
        pauseScreen.SetActive(true);
        //pipeSpawner.SetIsGameGoing(gameGoing);
        pipeSpawner.gameObject.SetActive(gameGoing);
        menuButton.SetActive(true);
    }

    public void UnpauseGame()
    {
        if (bird.GetIsAlive())
        {
            pauseScreen.SetActive(false);
            gameGoing = true;
            bird.SetIsGameGoing(gameGoing);
            //pipeSpawner.SetIsGameGoing(gameGoing);
            pipeSpawner.gameObject.SetActive(gameGoing);
            menuButton.SetActive(false);
        }
    }

    public void ReturnToMainPage()
    {
        SceneLoader.Load(SceneLoader.Scene.MainPage);
    }
}
