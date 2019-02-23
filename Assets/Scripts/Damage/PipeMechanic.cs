using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMechanic : Interactable
{
    public Transform pipeTransform;
    public Rigidbody pipeRB;
    public float damageThreshold;
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

    public override void InteractWith()
    {
        pickUpCommand();
        base.InteractWith();
    }

    public void GenerateThreshold()
    {
        damageThreshold = Random.Range(5f, 25f);
    }

    public void PipeBurst()
    {
        pipeRB.useGravity = true;
        pipeRB.AddForce(Vector3.right * 2.5f, ForceMode.Impulse);
        pipeRB.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        HullDamage.instance.hullIntegrity -= HullDamage.instance.pipeIntegrityDamage;
    }

    #region Effects
    public override void Wet()
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

        base.Wet();
    }

    public override void Electric()
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

        base.Electric();
    }

    public override void Goo()
    {
        goo = true;
        base.Goo();
    }

    public override void Blunt()
    {
        blunt = true;
        base.Blunt();
    }

    public override void Fire()
    {
        fire = true;
        base.Fire();
    }
    #endregion
}
