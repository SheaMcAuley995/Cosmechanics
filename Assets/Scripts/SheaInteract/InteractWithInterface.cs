using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractWithInterface : MonoBehaviour
{

    public Interactable interactableObject;
    public GameObject   interactedObject;
    public float radius;
    public LayerMask interactableLayer;

    [Space]
    [Header("Pick 'Um Up")]
    public GameObject puuPrefab;
    [HideInInspector] public GameObject puu;
    bool isPuu = false;


    public PlayerController controller;

    public void InteractWithObject()
    {
        if(interactedObject != null)
        {
            if(interactedObject.GetComponent<IInteractableTool>() != null)
            {
                interactedObject.GetComponent<IInteractableTool>().toolInteraction();
                Debug.Log(":");
            }
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                //Debug.Log("Interacting with :" + hitColliders[i].name);
                if (hitColliders[i].GetComponent<RepairableObject>() != null)
                {
                    if (hitColliders[i].GetComponent<RepairableObject>().health != hitColliders[i].GetComponent<RepairableObject>().healthMax)
                    {
                        controller.animators[0].SetTrigger("PipeFix");
                        controller.animators[1].SetTrigger("PipeFix");
                        hitColliders[i].GetComponent<IInteractable>().InteractWith();
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



    public void pickUpObject(Collider box)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
        if (interactedObject == null)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<PickUp>() != null)
                {
                    hitColliders[i].GetComponent<PickUp>().pickMeUp(transform);
                    hitColliders[i].GetComponent<PickUp>().playerController = controller;
                    interactedObject = hitColliders[i].gameObject;
                    controller.interactableObject = interactableObject;
                    isPuu = true;
                    puu = Instantiate(puuPrefab, interactedObject.transform.position, interactedObject.transform.rotation, interactedObject.transform);
                    box.enabled = true;

                    if (hitColliders[i].GetComponent<Interactable>() != false)
                    {
                        interactableObject = hitColliders[i].GetComponent<Interactable>();
                    }
                    break;
                }
            }
        }
        else
        {
            if (interactedObject.GetComponent<PickUp>() != null)
            {
                interactedObject.GetComponent<PickUp>().playerController = null;
            }
            interactedObject.GetComponent<PickUp>().putMeDown();
            isPuu = false;
            Destroy(puu);
            box.enabled = false;
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

}