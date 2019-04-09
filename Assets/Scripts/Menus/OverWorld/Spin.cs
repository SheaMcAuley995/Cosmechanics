using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    public const float rotationSpeed = 100.0f;

    [Range(-rotationSpeed, rotationSpeed)]
    public float rotateX;
    [Range(-rotationSpeed, rotationSpeed)]
    public float rotateY;
    [Range(-rotationSpeed, rotationSpeed)]
    public float rotateZ;




    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);
    }
}