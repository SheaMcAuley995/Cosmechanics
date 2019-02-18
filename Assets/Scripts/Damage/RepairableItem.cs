using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableItem : Interactable
{
    public LayerMask player;
    public Transform objectTransform;
    float repairRange = 5f;
    [SerializeField] float shieldRepairAmount = 10f;

    float shipIntegrity = HullDamage.instance.hullIntegrity;

    // Use this for initialization
    void Start ()
    {
		if (objectTransform == null)
        {
            objectTransform = GetComponent<Transform>();
        }
	}

    public override void InteractWith()
    {        
        //RaycastHit hit;
        //bool foundPlayer = Physics.Raycast(objectTransform.position, objectTransform.forward, out hit, repairRange, player, QueryTriggerInteraction.Collide);
        Collider[] thePlayer = Physics.OverlapSphere(transform.position, 5f, player, QueryTriggerInteraction.Collide);

        if (thePlayer != null)
        {
            if (gameObject.CompareTag("Crack"))
            {
                RepairCrack();
            }
            else if (gameObject.CompareTag("Dent"))
            {
                RepairDent();
            }
            else if (gameObject.CompareTag("Shield"))
            {
                Debug.Log("Repairing Shields");
                RepairShields();
            }
        }
        else
        {
            Debug.Log("There's no damage here ya dingus");
        }

        base.InteractWith();
    }

    void RepairCrack()
    {
        StartCoroutine(HullDamage.instance.RepairShipIntegrity(HullDamage.instance.crackIntegrityDamage));
    }

    void RepairDent()
    {
        StartCoroutine(HullDamage.instance.RepairShipIntegrity(HullDamage.instance.dentIntegrityDamage));
    }

    void RepairShields()
    {
        HullDamage.instance.RepairShieldCapacity(shieldRepairAmount);
    }
}
