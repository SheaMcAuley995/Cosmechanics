using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractWithInterface : MonoBehaviour
{

    public Interactable interactableObject;
    public float radius;
    public LayerMask interactableLayer;

    [Space]
    [Header("Pick 'Um Up")]
    public GameObject puuPrefab;
    GameObject puu;
    bool isPuu = false;

    GameObject interactedObject;
    public PlayerController controller;

    public void InteractWithObject()
    {
        if(interactedObject != null)
        {
            if(interactedObject.GetComponent<IInteractableTool>() != null)
            {
                interactedObject.GetComponent<IInteractableTool>().toolInteraction();
            }
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
            //Debug.Log("InteractWithObject()");

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<RepairableObject>() != null)
                {
                    if (hitColliders[i].GetComponent<RepairableObject>().health != hitColliders[i].GetComponent<RepairableObject>().healthMax)
                    {
                        controller.animators[0].SetBool("FixPipe", true);
                        controller.animators[1].SetBool("FixPipe", true);
                        hitColliders[i].GetComponent<IInteractable>().InteractWith();
                        controller.animators[0].SetBool("FixPipe", false);
                        controller.animators[1].SetBool("FixPipe", false);
                        break;
                    }
                }
                else
                {
                    if(hitColliders[i].GetComponent<IInteractable>() != null)
                    {
                        hitColliders[i].GetComponent<IInteractable>().InteractWith();
                    }
                    break;
                }

            }
        }
    }
        
    

    public void pickUpObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
        //Debug.Log("Calling pickUpObject()");
        //Debug.Log(hitColliders.Length);
        if(interactedObject == null)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                //  Debug.Log("Calling object " + hitColliders[i]);
                if (hitColliders[i].GetComponent<PickUp>() != null)
                {
                    hitColliders[i].GetComponent<PickUp>().pickMeUp(transform);
                    interactedObject = hitColliders[i].gameObject;
                    isPuu = true;
                    puu = Instantiate(puuPrefab, interactedObject.transform.position, interactedObject.transform.rotation, interactedObject.transform);

                    if (interactedObject.GetComponent<FireExtinguisher>() != null)
                    {
                        interactedObject.GetComponent<FireExtinguisher>().playerController = controller;
                    }

                    break;
                }
            }
        }
        else
        {
            if (interactedObject.GetComponent<FireExtinguisher>() != null)
            {
                interactedObject.GetComponent<FireExtinguisher>().playerController = null;
            }
            interactedObject.GetComponent<PickUp>().putMeDown();
            isPuu = false;
            Destroy(puu);
            interactedObject = null;
        }
        if (!isPuu)
        {
            Destroy(puu);
        }
    }


    public void callInteract()
    {
        if (interactableObject != null)
        {
            interactableObject.InteractWith();
        }
    }
    public void closeInteract()
    {
        interactableObject = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    //public void grabObject()
    //{
    //    if(interactedObject != null)
    //    {
    //        interactedObject.transform.position = transform.position;
    //        interactedObject.transform.eulerAngles = transform.eulerAngles;
    //    }
    //}
    //
    //public void throwObject()
    //{
    //    if (interactedObject != null)
    //    {
    //        interactedObject.GetComponent<Rigidbody>().velocity = transform.forward * throwForce;
    //    }
    //}

    //public void InteractWith()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
    //    Debug.Log("Calling InteractWith()");
    //    Debug.Log(hitColliders.Length);
    //    for (int i = 0; i < hitColliders.Length; i++)
    //    {
    //        // Chache this later once the check becomes larger
    //        Debug.Log("Calling object " + i);
    //        if (hitColliders[i].GetComponent<Interactable>() != null)
    //        {
    //            Interactable testedInteractable = hitColliders[i].GetComponent<Interactable>();
    //
    //            if (testedInteractable.pickedUp == false)
    //            {
    //                if (interactableObject == null)
    //                {
    //                    Debug.Log("Setting interactable to " + hitColliders[i]);
    //                    interactableObject = testedInteractable;
    //                    return;
    //                }
    //            }
    //
    //        }
    //    }
    //}

}