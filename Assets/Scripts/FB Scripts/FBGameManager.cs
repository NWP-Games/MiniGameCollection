using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FBGameManager : MonoBehaviour
{
    [SerializeField] private FBBirdScript bird;
    [SerializeField] private FBPipeSpawner pipeSpawner;
    [SerializeField] private bool gameGoing = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;
    
    public void Score() { score++; }

    private void ResetGame()
    {
        score = 0;
    }
}
