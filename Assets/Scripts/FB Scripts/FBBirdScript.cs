using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBBirdScript : MonoBehaviour
{
    [SerializeField] private bool isAlive = true;
    [SerializeField] private bool isGameGoing = false;
    [SerializeField] private FBGameManager gameManager;
    [SerializeField] private GameObject wingUp;
    [SerializeField] private GameObject wingDown;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 flapVector = new Vector2(0f, 5f);

    private void Start()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        if (!isAlive) return;
        if (!isGameGoing) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(flapVector, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerInt = collision.gameObject.layer;
        if(layerInt == LayerMask.NameToLayer("Pipe")) SetIsAlive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerInt = collision.gameObject.layer;
        if (layerInt == LayerMask.NameToLayer("Bounds"))
            SetIsAlive(false);
        else if (layerInt == LayerMask.NameToLayer("ScoreZone"))
            gameManager.Score();
    }

    public bool GetIsAlive() { return isAlive; }

    public void SetIsAlive(bool isAlive) 
    { 
        this.isAlive = isAlive; 
        if(!isAlive)
        {
            gameManager.GameOver();
        }
    }

    public void SetIsGameGoing(bool isGameGoing) 
    { 
        this.isGameGoing = isGameGoing;

        if(!isGameGoing)
        {
            rb.bodyType = RigidbodyType2D.Static;

        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
