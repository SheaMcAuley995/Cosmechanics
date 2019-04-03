using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

    [Header("Engine Statistics")]
    public float engineHeat;
    public float maxHeat;
    public float engineCoolingAmount;
    [SerializeField] [Range(0, 1)] float florpCoolingPercentage;

    [Header("Win Condition")]
    public GameObject winGameUI;
    public float winConditionLimit;
    public float currentProgress;
    public float enemyProgress;
    public float progressionMultiplier;

    public Slider slider;
    public AlertUI alertUI;

    [Header("Debug Tools")]
    public bool testInputFlorp = false;
    public void Start()
    {
        winGameUI.SetActive(false);
        engineHeat = maxHeat / 2;
        currentProgress = winConditionLimit / 8;
        alertUI.problemMax = maxHeat;
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
        engineHeat = Mathf.Clamp(engineHeat, 0, maxHeat);

        if(currentProgress > winConditionLimit)
        {
            winGameUI.SetActive(true);
            BroadcastMessage("StopGame");
        }

        alertUI.problemCurrent = engineHeat;
    }

    private void StopGame()
    {
        enabled = false;
    }

    public void InsertFlorp()
    {
        engineHeat = Mathf.Clamp(engineHeat += maxHeat * florpCoolingPercentage, 0, maxHeat);
    }

    public float engineHeatPercentage()
    {
        return Mathf.Clamp((engineHeat / maxHeat) * 100, 0, 100);
    }
}
