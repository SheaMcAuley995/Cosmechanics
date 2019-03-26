using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

    [Header("Engine Statistics")]
    public float engineHeat;
    public float maxHeat;
    public float engineCoolingAmount;

    [Header("Win Condition")]
    public float winConditionLimit;
    public float currentProgress;
    public float enemyProgress;
    public float progressionMultiplier;

    public Slider slider;

    [Header("Debug Tools")]
    public bool testInputFlorp = false;
    public void Start()
    {
        engineHeat = maxHeat / 2;
        currentProgress = winConditionLimit / 4;
    }

    public void Update()
    {
        engineHeat -= Time.deltaTime * engineCoolingAmount;
        if(testInputFlorp)
        {
            InsertFlorp();
            testInputFlorp = false;
        }

        currentProgress += Time.deltaTime * engineHeatPercentage() * progressionMultiplier;
        slider.value = currentProgress / winConditionLimit;
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
