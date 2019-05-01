using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour, IPooledObject
{
    public Vector3 startingPosition;
    public Vector3 direction;
    public float shipSpeed = 1;

    public void Start()
    {
        startingPosition = transform.position;
    }

    public Vector3 shipFlightDirecton()
    {
        return (direction - transform.position).normalized * shipSpeed;
    }

    public void OnObjectSpawn()
    {
        transform.position = startingPosition;
    }

    public void Update()
    {
        transform.Translate(transform.InverseTransformDirection(shipFlightDirecton()));
    }

}
