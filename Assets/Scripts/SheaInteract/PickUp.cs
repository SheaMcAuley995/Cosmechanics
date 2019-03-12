using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void pickMeUp(Transform pickUpTransform)
    {
        rb.isKinematic = true;
        transform.SetParent(pickUpTransform);
        transform.position = pickUpTransform.position;
    }
}
