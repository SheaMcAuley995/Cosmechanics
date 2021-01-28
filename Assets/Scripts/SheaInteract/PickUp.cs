using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Android;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PickUp : MonoBehaviour
{
    public Rigidbody rb;
    public PlayerController playerController;
    public Collider myCollider;
    public Transform holdPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
    }

    public virtual void pickMeUp(Transform pickUpTransform)
    {
        if (playerController != null)
        {
            playerController.interactedObject = gameObject;
            playerController.interact.interactedObject = null;
        }
        myCollider.enabled = false;
        rb.isKinematic = true;
        transform.SetParent(pickUpTransform);
        transform.position = pickUpTransform.position;
        transform.eulerAngles = pickUpTransform.eulerAngles;
    }

    public virtual void myInteraction()
    {
        if (playerController != null) { playerController.blockMovement = true; }
    }

    public virtual void endMyInteraction()
    {
        if (playerController != null) { playerController.blockMovement = false; }
    }

    public virtual void putMeDown(float force)
    {
        //Debug.Log(force);
        endMyInteraction();
        playerController = null;
        myCollider.enabled = true;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.velocity = (transform.forward).normalized * force;
    }
}

