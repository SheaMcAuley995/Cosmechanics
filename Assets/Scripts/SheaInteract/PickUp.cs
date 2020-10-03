using System.Collections;
using System.Collections.Generic;
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
        if (myCollider == null)
        {
            myCollider = GetComponent<Collider>();
        }
        if (playerController != null)
        {
            playerController.interact.interactedObject = null;
            Destroy(playerController.interact.puu);
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

    public virtual void putMeDown()
    {
        playerController = null;
        myCollider.enabled = true;
        transform.SetParent(null);
        rb.isKinematic = false;
        endMyInteraction();
    }
}

