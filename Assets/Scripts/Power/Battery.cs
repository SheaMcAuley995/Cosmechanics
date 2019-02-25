using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IPickUpable, IInteractable, IDropable, IStatusEffect
{
    [Header("Battery Life")]    
    public float batteryLife;
    static float fullBatteryLife = 100f;
    static float minBatteryLife = 0f;

    [Header("Charge Modifiers")]
    [SerializeField] float initialDrainRate = 2f;
    [SerializeField] float initialRechargeRate = 1f;
    [SerializeField] float doubleDrainRate = 4f;
    [SerializeField] float doubleRechargeRate = 2f;
    [SerializeField] float slowDrainRate = 1f;
    [SerializeField] float slowRechargeRate = 0.5f;
    float drainRate;
    float rechargeRate;

    [Header("Battery Status")]
    /*[HideInInspector]*/ public bool isSupplyingPower;
    /*[HideInInspector]*/ public bool isCharging;

    public LayerMask charingDock;
    float batteryPlugDistance = 2f;


    void Start()
    {
        batteryLife = fullBatteryLife;
        drainRate = initialDrainRate;
        rechargeRate = initialRechargeRate;
    }

    public void PickUp()
    {
        // Pickup code (NOTE: Needs re-work from Shea first)
    }

    public void DropObject()
    {
        // See PickUp()
    }

    public void InteractWith()
    {
        Collider[] chargeLocations = Physics.OverlapSphere(transform.position, batteryPlugDistance, charingDock);
        if (chargeLocations != null)
        {

        }
    }

    void CheckBatteryStatus()
    {
        if (isSupplyingPower)
        {
            DrainBatteryLife();
        }

        if (isCharging)
        {
            RechargeBattery();
        }
    }

    void Update()
    {
        CheckBatteryStatus();
    }

    #region Battery Life Functions
    void DrainBatteryLife()
    {
        if (batteryLife > minBatteryLife)
        {
            batteryLife -= drainRate * Time.deltaTime;
        }
    }

    void RechargeBattery()
    {
        if (batteryLife < fullBatteryLife)
        {
            batteryLife += rechargeRate * Time.deltaTime;
        }
    }
    #endregion

    #region Status Effects
    public void Wet()
    {
        
    }

    public void Electricity()
    {
        rechargeRate = doubleRechargeRate;
        drainRate = slowDrainRate;
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
}
