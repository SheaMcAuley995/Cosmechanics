using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [Header("Battery Life")]    
    public float batteryLife;
    static float fullBatteryLife = 100f;
    static float minBatteryLife = 0f;


    [Header("Battery Status")]
    /*[HideInInspector]*/ public bool isSupplyingPower;
    /*[HideInInspector]*/ public bool isCharging;

    public LayerMask charingDock;

    [SerializeField] Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        batteryLife = 0;
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

    public void PlugBattery(Transform lockToTrans, typeOfPort type)
    {
        
        transform.position = lockToTrans.position;
        transform.eulerAngles = lockToTrans.eulerAngles;
        transform.SetParent(lockToTrans);
        if(type == typeOfPort.charge)
        {
            isCharging = true;
        }
        if(type == typeOfPort.charge)
        {
            isSupplyingPower = true;
        }

    }

    public void unPlugBattery()
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
