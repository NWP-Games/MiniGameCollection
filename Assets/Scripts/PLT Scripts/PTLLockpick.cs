using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTLLockpick : MonoBehaviour
{
    [SerializeField] private int rotationDirection = 1;
    [SerializeField] private float rotationSpeed = 20.0f;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private GameObject currentUnlockPoint;
    [SerializeField] private bool gameGoing = false;

    private void Start()
    {
        capsuleCollider = this.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!gameGoing) return;

        LockpickRotation();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rotationDirection = rotationDirection * -1;
        }
    }

    private void LockpickRotation()
    {
        this.transform.RotateAround(Vector3.zero, new Vector3(0, 0, 1) * rotationDirection, rotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentUnlockPoint = collision.gameObject;
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentUnlockPoint = null;
    }
    */
    public GameObject GetCurrentUnlockPoint() { return currentUnlockPoint; }

    public void SetGameGoing(bool gameGoing) { this.gameGoing = gameGoing; }

    public void IncreaseSpeed() { rotationSpeed += 1f; }

    public void ResetSpeed() { rotationSpeed = 20.0f; }
    
}
