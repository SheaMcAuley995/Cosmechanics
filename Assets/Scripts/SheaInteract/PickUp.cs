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

    public void pickMeUp(Transform pickUpTransform)
    {
        rb.isKinematic = true;
        transform.SetParent(pickUpTransform);
        transform.position = pickUpTransform.position;
        transform.eulerAngles = pickUpTransform.eulerAngles;
        AudioEventManager.instance.PlaySound("splat", .3f, Random.Range(.5f, .7f), 0);
    }

    public void putMeDown()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
    }
}
