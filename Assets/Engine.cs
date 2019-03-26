using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

    public float engineHeat;
    public float maxHeat;
    public float engineCoolingAmount;

    public bool testInputFlorp = false;
    public void Start()
    {
        engineHeat = maxHeat / 2;
    }

    public void Update()
    {
        engineHeat -= Time.deltaTime * engineCoolingAmount;
        if(testInputFlorp)
        {
            InsertFlorp();
            testInputFlorp = false;
        }
    }

    public void InsertFlorp()
    {
        engineHeat = Mathf.Clamp(engineHeat += 25, 0, maxHeat);
    }

    public float engineHeatPercentage()
    {
        return Mathf.Clamp((engineHeat / maxHeat) * 100, 0, 100);
    }
}
