using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable, IPickUpable {

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
    public void PickUp()
    {

    }
    public void DropObject()
    {

    }

    IEnumerator UpdatePickup()
    {
        while(true)
        {
            rb.velocity = (travelToTransform.position - transform.position).normalized;
            yield return null;
        }
    }

}
