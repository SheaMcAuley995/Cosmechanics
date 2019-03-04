using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
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

    public Transform chargingPos;


    void Awake()
    {
        batteryLife = fullBatteryLife;
        drainRate = initialDrainRate;
        rechargeRate = initialRechargeRate;
    }

    public override void InteractWith()
    {
        pickUpCommand();

        base.InteractWith();
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

        float dist = Vector3.Distance(transform.position, chargingPos.transform.position);
        if (dist <= 1f)
        {
            PlugBattery();
        }
    }

    void PlugBattery()
    {
        transform.position = chargingPos.position;
        transform.parent = chargingPos;
        thisRB.isKinematic = true;
        Debug.Log("plugg");
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
