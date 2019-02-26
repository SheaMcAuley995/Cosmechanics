using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMechanic : Interactable
{
    public Transform pipeTransform;
    public Rigidbody pipeRB;
    public float damageThreshold;
    Transform pipeParent;
    Vector3 startPos, startRot;
    bool wet, electric, goo, fire, blunt;
    public bool isDamaged;
    public GameObject pipeEffect;

    void Awake()
    {
        pipeRB.isKinematic = true;

        if (pipeTransform == null)
        {
            pipeTransform = GetComponent<Transform>();
        }

        pipeParent = pipeTransform.parent;
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
        GameObject newEffect = Instantiate(pipeEffect, transform.position, transform.rotation);
        Destroy(newEffect, 3f);
        pipeRB.isKinematic = false;
        switch (pickedUp)
        {
            case false:
                StartCoroutine(PickupUpdate());
                break;
            case true: 
                StopCoroutine(PickupUpdate());
                break;
        }

        base.InteractWith();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, pipeParent.transform.position);
        if (dist <= 0.3f && !isDamaged)
        {
            FixPipe();
        }
    }

    public void GenerateThreshold()
    {
        damageThreshold = Random.Range(5f, 25f);
    }

    public void PipeBurst()
    {
        isDamaged = true;
        pipeTransform.parent = null;
        pipeRB.AddForce(Vector3.left * 10f, ForceMode.Impulse);
        pipeRB.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        HullDamage.instance.hullIntegrity -= HullDamage.instance.pipeIntegrityDamage;
    }

    void FixPipe()
    {
        pipeRB.isKinematic = true;
        isDamaged = false;
        pipeRB.useGravity = true;
        gameObject.transform.parent = pipeParent.transform;
        gameObject.transform.position = startPos;
        gameObject.transform.eulerAngles = startRot;
        HullDamage.instance.hullIntegrity += HullDamage.instance.pipeIntegrityDamage;
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
