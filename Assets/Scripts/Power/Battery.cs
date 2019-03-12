using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
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

    [SerializeField] Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        batteryLife = 0;
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

    public void PlugBattery(Transform lockToTrans)
    {
        
        transform.position = lockToTrans.position;
        transform.eulerAngles = lockToTrans.eulerAngles;
        transform.SetParent(lockToTrans);
        isCharging = true;
    }

    void unPlugBattery()
    {
        isCharging = false;
        isSupplyingPower = false;
    }


    #region Battery Life Functions
    void DrainBatteryLife()
    {
        rb.isKinematic = true;
        if (batteryLife > minBatteryLife)
        {
            batteryLife -= Time.deltaTime;
        }
    }

    void RechargeBattery()
    {
        rb.isKinematic = true;
        if (batteryLife < fullBatteryLife)
        {
            batteryLife += Time.deltaTime;
        }
    }
    #endregion

}
