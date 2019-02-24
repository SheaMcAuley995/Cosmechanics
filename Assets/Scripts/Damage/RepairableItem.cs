using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableItem : Interactable
{
    public LayerMask player;
    public Transform objectTransform;
    [HideInInspector] public bool wallIsDamaged;
    float repairRange = 5f;
    [SerializeField] float shieldRepairAmount = 10f;

    // Use this for initialization
    void Awake ()
    {
        objectTransform = GetComponent<Transform>();
    }

    public override void InteractWith()
    {        
        Collider[] thePlayer = Physics.OverlapSphere(transform.position, 5f, player, QueryTriggerInteraction.Collide);

        if (thePlayer != null)
        {
            if (wallIsDamaged)
            {
                /// Do these first two things in the coroutine after prototype
                wallIsDamaged = false;
                gameObject.GetComponent<Renderer>().material.mainTexture = HullDamage.instance.repairedTexture;
                RepairShip();
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

    void RepairShip()
    {
        StartCoroutine(HullDamage.instance.RepairShipIntegrity(HullDamage.instance.shipIntegrityDamage));
    }

    void RepairShields()
    {
        HullDamage.instance.RepairShieldCapacity(shieldRepairAmount);
    }
}
