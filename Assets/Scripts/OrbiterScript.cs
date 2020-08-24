using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterScript : MonoBehaviour
{
    public Transform center;
    public float angle;
    Vector3 centerPos;

    public bool doOrbit;
    public bool moveBack;


    Vector3 selectionPos;
    public Vector3 offset;

    Vector3 startingPos;


    float flightSpeed = .2f;

    public float rotationSpeed;

    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        centerPos = center.localPosition;

        selectionPos = centerPos + offset;

        startingPos = transform.localPosition;

        doOrbit = true;
        moveBack = false;
    }
    float counter;
    private void FixedUpdate()
    {
        if(moveBack == true)
        {
            counter += .1f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startingPos, counter);
            if(transform.localPosition == startingPos)
            {
                counter = 0;
                moveBack = false;
            }
        }

        if (doOrbit == true && moveBack == false)
        {
            transform.localPosition = new Vector3(Mathf.Cos(angle) * (transform.localPosition.x - centerPos.x) - Mathf.Sin(angle) * (transform.localPosition.z - centerPos.z) + centerPos.x, transform.localPosition.y,
                                                  Mathf.Sin(angle) * (transform.localPosition.x - centerPos.x) + Mathf.Cos(angle) * (transform.localPosition.z - centerPos.z) + centerPos.z);
            angle = Mathf.Lerp(0, 1, Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, selectionPos, ref velocity, flightSpeed);
        }

        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
    }
}