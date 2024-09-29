using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 targetPosition;

    private float movingSpeed;

    public void Init(Vector3 targetPosition, float movingSpeed)
    {
        this.targetPosition = targetPosition;
        this.movingSpeed = movingSpeed;    
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,targetPosition,movingSpeed * Time.deltaTime);
    }
}
