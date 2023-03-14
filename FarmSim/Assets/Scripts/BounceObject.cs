using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    public float bounceHeight = 1f;  // adjust the height of the bounce as desired
    public float bounceSpeed = 1f;   // adjust the speed of the bounce as desired

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight + startingPosition.y;
        transform.position = new Vector3(startingPosition.x, newY, startingPosition.z);
    }
}