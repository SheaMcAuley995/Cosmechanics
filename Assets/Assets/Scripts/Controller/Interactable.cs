using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType { Pickup, InteractOnce }
public class Interactable : MonoBehaviour
{

    public delegate void MyCurrentInteractions();
    MyCurrentInteractions myCurrentInteractions;

    public InteractionType myInteractionType;
    public Transform pickUpTransform;
    public Rigidbody thisRB;
    public bool pickedUp;
    public float pickUpSpeed = 0.1f;

    /// PUTTING OFF CAUSE NO ANIMATIONS
    /// <summary>
    /// Needs to call for a animation clip
    /// </summary>
    ///

    public void Start()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            thisRB = GetComponent<Rigidbody>();
        }
    }

    public virtual void InteractWith()
    {
        //Debug.Log("Calling base InteractWith");
        //Debug.Log("I love myself, Even tho I look like a burnt chicken nuget. I still love myself.");
    }

    public virtual void PickUp()
    {
        if (myInteractionType == InteractionType.Pickup)
        {
            if (pickUpTransform != null)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                transform.SetParent(pickUpTransform);
                thisRB.isKinematic = true;
                transform.position = Vector3.Lerp(transform.position, pickUpTransform.position, Time.deltaTime * pickUpSpeed); //pickUpTransform.position;
                transform.eulerAngles = pickUpTransform.eulerAngles;
                pickedUp = true;
            }
            else
            {
                if (thisRB != null)
                {
                    transform.SetParent(null);
                    thisRB.isKinematic = false;
                }
                GetComponent<CapsuleCollider>().enabled = true;
                pickedUp = false;
            }
        }

    }
    public void pickUpCommand()
    {
        switch (pickedUp)
        {
            case false:
                StartCoroutine(PickupUpdate());
                break;
            case true:
                StopCoroutine(PickupUpdate());
                break;
        }
    }

    public IEnumerator PickupUpdate()
    {
        while (true)
        {
            PickUp();
            yield return null;
        }
    }


    #region Effects
    public virtual void Wet()
    {
        return;
    }

    public virtual void Electric()
    {
        return;
    }

    public virtual void Goo()
    {
        return;
    }

    public virtual void Blunt()
    {
        return;
    }

    public virtual void Fire()
    {
        return;
    }
    #endregion
}