using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField]
    private float frequency = 5f;

    [SerializeField]
    private float magnitude = 5f;

    [SerializeField] 
    private float offset = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
    }
}