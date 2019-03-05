using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable {

    public Transform travelToTransform;
    public float travelToSpeed;
    private Rigidbody rb;
    //public myCurrentInteractions

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void InteractWith()
    {
        Debug.Log("Calling interface");
    }

}
