using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PTLUnlockPoint : MonoBehaviour
{
    private PTLGameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<PTLGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("unlock point trigger enter");
        if(collision.gameObject.name.Contains("UnlockPoint"))
        {
            gameManager.SpawnUnlockPoint();
            this.IsDestroyed();
        }
    }
}
