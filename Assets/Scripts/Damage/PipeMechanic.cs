using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMechanic : MonoBehaviour, IPickUpable, IInteractable, IStatusEffect
{
    public Transform pipeTransform;
    public Rigidbody pipeRB;
    public float damageThreshold;
    [HideInInspector] public bool isDamaged;
    Vector3 startPos, startRot;
    bool wet, electric, goo, fire, blunt;

    void Awake()
    {
        if (pipeTransform == null)
        {
            pipeTransform = GetComponent<Transform>();
        }

        startPos = pipeTransform.position;
        startRot = pipeTransform.rotation.eulerAngles;

        if (pipeRB == null)
        {
            pipeRB = GetComponent<Rigidbody>();
        }

        GenerateThreshold();
    }

    public void PickUp()
    {

    }

    public void InteractWith()
    {

    }

    #region Virtual Overrides
    //public override void PickUp()
    //{
    //    pickUpCommand();
    //    base.PickUp();
    //}

    //public override void InteractWith()
    //{
    //    Debug.Log("Uh");
    //}
    #endregion

    public void GenerateThreshold()
    {
        damageThreshold = Random.Range(5f, 25f);
    }

    public void PipeBurst()
    {
        isDamaged = true;
        pipeRB.useGravity = true;
        //pipeRB.AddForce(Vector3.right * 2.5f, ForceMode.Impulse);
        pipeRB.AddForce(Vector3.up * 1f, ForceMode.Impulse);
        HullDamage.instance.hullIntegrity -= HullDamage.instance.pipeIntegrityDamage;
    }

    #region Effects
    public void Wet()
    {
        wet = true;
        // Maybe apply a wet shader? 

        if (fire)
        {
            fire = false;
        }

        if (goo)
        {
            // Something cool
        }
    }

    public void Electricity()
    {
        electric = true;
        
        if (wet)
        {
            Debug.Log("You 'bout to get electorcuted ya goofus");
        }

        if (goo)
        {
            // Something cool
        }
    }

    public void Florp()
    {
        goo = true;
    }

    public void Blunt()
    {
        blunt = true;
    }

    public void Fire()
    {
        fire = true;
    }
    #endregion
}
