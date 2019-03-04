using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public Rigidbody nice;

    public Interactable interactableObject;
    public float radius;
    public LayerMask interactableLayer;

<<<<<<< dev
  public void InteractWith()
  {
      Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
      Debug.Log("Calling InteractWith()");
      Debug.Log(hitColliders.Length);
      for (int i = 0; i < hitColliders.Length; i++)
      {
          // Chache this later once the check becomes larger
          Debug.Log("Calling object " + i);
          if (hitColliders[i].GetComponent<Interactable>() != null)
          {
              Interactable testedInteractable = hitColliders[i].GetComponent<Interactable>();
  
              if (testedInteractable.pickedUp == false)
              {
                  if (interactableObject == null)
                  {
                      Debug.Log("Setting interactable to " + hitColliders[i]);
                      interactableObject = testedInteractable;
                      return;
                  }
              }
  
          }
      }
  }

    //public void InteractWith()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
    //    Debug.Log("Calling InteractWith()");
    //    Debug.Log(hitColliders.Length);
    //    for (int i = 0; i < hitColliders.Length; i++)
    //    {
    //        Debug.Log("Calling object " + hitColliders[i]);
    //        if (hitColliders[i].GetComponent<IInteractable>() != null)
    //        {
    //            hitColliders[i].GetComponent<IInteractable>().InteractWith();
    //        }
    //    }
    //}

=======
   public void InteractWith()
   {
       Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
       Debug.Log("Calling InteractWith()");
       Debug.Log(hitColliders.Length);
       for (int i = 0; i < hitColliders.Length; i++)
       {
           // Chache this later once the check becomes larger
           Debug.Log("Calling object " + i);
           if (hitColliders[i].GetComponent<Interactable>() != null)
           {
               Interactable testedInteractable = hitColliders[i].GetComponent<Interactable>();
   
               if (testedInteractable.pickedUp == false)
               {
                   if (interactableObject == null)
                   {
                       Debug.Log("Setting interactable to " + hitColliders[i]);
                       interactableObject = testedInteractable;
                       return;
                   }
               }
   
           }
       }
   }

   //public void InteractWith()
   //{
   //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
   //    Debug.Log("Calling InteractWith()");
   //    Debug.Log(hitColliders.Length);
   //    for (int i = 0; i < hitColliders.Length; i++)
   //    {
   //        Debug.Log("Calling object " + hitColliders[i]);
   //        if (hitColliders[i].GetComponent<IInteractable>() != null)
   //        {
   //            hitColliders[i].GetComponent<IInteractable>().InteractWith();
   //        }
   //    }
   //}

>>>>>>> WIP of new Interact scripts

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

}