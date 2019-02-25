using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableItem : MonoBehaviour, IPickUpable, IInteractable, IDropable, IStatusEffect
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

    public void InteractWith()
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
    }

    void RepairShip()
    {
        StartCoroutine(HullDamage.instance.RepairShipIntegrity(HullDamage.instance.wallIntegrityDamage));
    }

    void RepairShields()
    {
        HullDamage.instance.RepairShieldCapacity(shieldRepairAmount);
    }

    #region Effects
    public void Wet()
    {

    }

    public void Electricity()
    {

    }

    public void Florp()
    {

    }

    public void Blunt()
    {

    }

    public void Fire()
    {

    }
    #endregion

    #region Functions Needed To Satisfy The Inferfaces But Won't Be Used Cause You Can't Pickup Walls
    public void PickUp()
    {
        // Send some sort of flashy message that you can't pick up walls
    }

    public void DropObject()
    {
        // See above
    }
    #endregion
}
