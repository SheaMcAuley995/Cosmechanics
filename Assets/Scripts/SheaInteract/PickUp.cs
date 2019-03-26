using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUp : MonoBehaviour {

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void pickMeUp(Transform pickUpTransform)
    {
        rb.isKinematic = true;
        transform.SetParent(pickUpTransform);
        transform.position = pickUpTransform.position;
        transform.eulerAngles = pickUpTransform.eulerAngles;
       
    }

    public void putMeDown()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
    }
}
