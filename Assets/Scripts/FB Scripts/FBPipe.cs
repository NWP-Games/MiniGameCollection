using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBPipe : MonoBehaviour
{
    [SerializeField] private int targetXPosition = -10;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(targetXPosition, transform.position.y, transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, Time.deltaTime);
        if (this.transform.position == targetPosition) Destroy(this.gameObject);
    }
}
