using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour {


    public static Engine instance;
    [Header("Engine Statistics")]
    public float engineHeat;
    public float maxHeat;
    public float engineCoolingAmount;
    [Range(0, 1)] public float florpCoolingPercentage;

    [HideInInspector] public bool startEngineBehavior = false;

    [Header("Win Condition")]
    public float winConditionLimit;
    public float currentProgress;
    public float enemyProgress;
    public float progressionMultiplier;
    public float enemyProgressionMultiplier;

    public Slider ShipProgressSlider;
    public Slider enemyShipProgressSlider;
    public AlertUI alertUI;

    [Header("Debug Tools")]
    public bool testInputFlorp = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Start()
    {
        engineHeat = maxHeat * 0.75f;
        currentProgress = winConditionLimit / 25;
        alertUI.problemMax = maxHeat;
    }

    private void Update()
    {
        // If the engine event can run & the game isn't paused
        if(startEngineBehavior && GameStateManager.instance.GetState() != GameState.Paused)
        {
            EngineUpdate();
        }
    }

    public void EngineUpdate()
    {
        
        engineHeat -= Time.deltaTime * engineCoolingAmount;
        
        if(testInputFlorp)
        {
            InsertFlorp();
            testInputFlorp = false;
        }

        currentProgress += Time.deltaTime * engineHeatPercentage() * progressionMultiplier;
        enemyProgress += Time.deltaTime * 100 * enemyProgressionMultiplier;
        ShipProgressSlider.value = currentProgress / winConditionLimit;
        enemyShipProgressSlider.value = enemyProgress / winConditionLimit;
        engineHeat = Mathf.Clamp(engineHeat, 0, maxHeat);


        if(currentProgress > winConditionLimit)
        {
            WinGame();
        }
        if(enemyProgress > currentProgress)
        {
            LoseGame();
        }
        AudioEventManager.instance.PlaySound("engine");
        alertUI.problemCurrent = engineHeat;
    }

    private void WinGame()
    {
        SceneFader.instance.FadeTo("WinScene");
        //ASyncManager.instance.winOperation.allowSceneActivation = true;
    }

    private void LoseGame()
    {
        SceneFader.instance.FadeTo("LoseScene");
        GameStateManager.instance.SetGameState(GameState.LostByFlorp);
        //ASyncManager.instance.loseOperation.allowSceneActivation = true;
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
