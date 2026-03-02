using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingEffect : MonoBehaviour
{
    public float amplitude = 0.1f; // How far up/down it moves
    public float frequency = 1.0f; // How fast it moves

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        
        // Apply the position
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
