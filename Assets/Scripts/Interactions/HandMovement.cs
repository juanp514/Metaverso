using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public float speed = 5f;         
    public float distance = 0.0739f;      

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; 
    }

    void Update()
    {
        
        Vector3 newPosition = startPosition;
        newPosition.x += Mathf.Sin(Time.time * speed) * distance;

        
        transform.position = newPosition;
    }
}
