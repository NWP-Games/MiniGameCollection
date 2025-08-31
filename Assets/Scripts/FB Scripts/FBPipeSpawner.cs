using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FBPipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipe;
    [SerializeField] private int spawnInterval = 3;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float heightVariationBounds = 4f;
    [SerializeField] private bool isGameGoing = false;

    private void Start()
    {
        float randomY = transform.position.y + Random.Range(heightVariationBounds * -1, heightVariationBounds);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        Instantiate(pipe, spawnPosition, Quaternion.identity, this.transform);
    }

    private void Update()
    {
        if (!isGameGoing) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            float randomY = transform.position.y + Random.Range(heightVariationBounds * -1, heightVariationBounds);
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
            Instantiate(pipe, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    public void SetIsGameGoing(bool isGameGoing)
    {
        this.isGameGoing = isGameGoing;
    }

    public void DestoryPipes()
    {
        FBPipe[] pipes = this.GetComponentsInChildren<FBPipe>();
        foreach(FBPipe pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
    }
}
