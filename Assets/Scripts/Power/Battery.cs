using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    Wet, Electric, Goo, Blunt, Fire
}

public class Battery : Interactable
{
    StatusEffect status;

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

    public override void InteractWith()
    {
        pickUpCommand();
        base.InteractWith();
    }

    public override void PickUp()
    {
        base.PickUp();
    }

    public Battery(StatusEffect Status)
    {
        status = Status;
    }

    void Start()
    {
        batteryLife = fullBatteryLife;
        drainRate = initialDrainRate;
        rechargeRate = initialRechargeRate;
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

    #region Interact Functions
    public override void Wet()
    {
        Debug.Log("Nothing");
    }

    public override void Electric()
    {
        rechargeRate = doubleRechargeRate;
        drainRate = slowDrainRate;
    }

    public override void Goo()
    {
        Debug.Log("Do Goo Thing");
    }

    public override void Blunt()
    {
        Debug.Log("Do Blunt Thing");
    }

    public override void Fire()
    {
        Debug.Log("Do Fire Thing");
    }
    #endregion
}
