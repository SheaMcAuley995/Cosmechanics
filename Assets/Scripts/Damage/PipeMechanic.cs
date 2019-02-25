using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMechanic : Interactable
{
    [Header("Dependencies & Components")]
    public Transform pipeTransform;
    public Rigidbody pipeRB;

    [Header("Settings")]
    [Tooltip("WARNING: DON'T SET MANUALLY")] public float damageThreshold;
    public bool isDamaged;

    Vector3 startPos, startRot;
    Transform pipeParent;

    bool wet, electricity, florp, fire, blunt;

    void Awake()
    {
        if (pipeTransform == null)
        {
            pipeTransform = GetComponent<Transform>();
        }
        if (pipeRB == null)
        {
            pipeRB = GetComponent<Rigidbody>();
        }

        pipeParent = gameObject.transform.parent;
        startPos = pipeTransform.position;
        startRot = pipeTransform.rotation.eulerAngles;

        GenerateDamageThreshold();
    }

    public override void PickUp()
    {
        //pickUpCommand();

        base.PickUp();
    }

    public override void InteractWith()
    {
        Debug.Log("IntWith");
        pickUpCommand();
        //float dist = Vector3.Distance(pipeParent.transform.position, gameObject.transform.position);
        //if (dist <= 2f)
        //{
        //    FixPipe();
        //}

        base.InteractWith();
    }

    void Update()
    {
        float dist = Vector3.Distance(pipeParent.transform.position, gameObject.transform.position);

        if (dist <= 2f && isDamaged)
        {
            FixPipe();
        }
    }

    public void GenerateDamageThreshold()
    {
        damageThreshold = Random.Range(5f, 25f);
    }

    public void PipeBurst()
    {
        isDamaged = true;
        pipeRB.useGravity = true;
        gameObject.transform.parent = null;
        pipeRB.AddForce(Vector3.right * 2f, ForceMode.Impulse);
        pipeRB.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
        HullDamage.instance.hullIntegrity -= HullDamage.instance.pipeIntegrityDamage;
    }

    void FixPipe()
    {
        isDamaged = false;
        pipeRB.useGravity = false;
        gameObject.transform.parent = pipeParent;
        gameObject.transform.position = startPos;
        gameObject.transform.eulerAngles = startRot;
        HullDamage.instance.hullIntegrity += HullDamage.instance.pipeIntegrityDamage;
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

        if (florp)
        {
            // Something cool
        }
    }

    public void Electricity()
    {
        electricity = true;
        
        if (wet)
        {
            Debug.Log("You 'bout to get electorcuted ya goofus");
        }

        if (florp)
        {
            // Something cool
        }
    }

    public void Florp()
    {
        florp = true;
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
