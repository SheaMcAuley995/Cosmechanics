using Rewired.Demos;
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
    public bool isFueled = false;

    [Header("Win Condition")]
    //public JumpToHyperSpace jumpScript;
    public float winConditionLimit;
    public float currentProgress;
    public float enemyProgress;


    public Slider ShipProgressSlider;
    public Slider enemyShipProgressSlider;
    //public AlertUI alertUI;

    // Zach additions.
   //[Header("Effects")]
   //[SerializeField] ParticleSystem[] starFieldEffects;
   //[SerializeField] float maxSimulationSpeed = 4.0f;

    [Header("Debug Tools")]
    public bool testInputFlorp = false;

    public GameObject loseGameScreen;
    public GameObject winGameScreen;

    private float time;
    private float timeSinceLastEvent;

    private float preEnemyProgress;
    private float preCurrentProgress;
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
        GameplayLoopManager.onNextTickEvent += EngineUpdate;
        //engineHeat = maxHeat * 0.75f;
        currentProgress = 2;
        //alertUI.problemMax = maxHeat;
    }

    private void Update()
    {
        ShipProgressSlider.value = Mathf.Lerp(ShipProgressSlider.value, currentProgress / winConditionLimit, time / GameplayLoopManager.TimeBetweenEvents);
        enemyShipProgressSlider.value = Mathf.Lerp(enemyShipProgressSlider.value, enemyProgress / winConditionLimit, time / GameplayLoopManager.TimeBetweenEvents);
        time += Time.deltaTime;

        //Debug.Log(time / GameplayLoopManager.TimeBetweenEvents);
    }

    public void EngineUpdate()
    {

        if (testInputFlorp)
        {
            InsertFlorp();
            testInputFlorp = false;
        }

        preEnemyProgress = enemyProgress;
        preCurrentProgress = currentProgress;
        time = 0;
        if (isFueled) { currentProgress += 1.3f; }
        enemyProgress += 1;

        //currentProgress += Time.deltaTime * engineHeatPercentage() * progressionMultiplier;
        //enemyProgress += Time.deltaTime * 100 * enemyProgressionMultiplier;

        engineHeat = Mathf.Clamp(engineHeat, 0, maxHeat);


        if (Engine.instance.currentProgress > Engine.instance.winConditionLimit)
        {
            Engine.instance.WinGame();
        }
        if (Engine.instance.enemyProgress > Engine.instance.currentProgress)
        {
            Engine.instance.LoseGame();
        }
    }

    private void WinGame()
    {
        //StartCoroutine(jumpScript.HyperspaceJump());
        winGameScreen.SetActive(true);
        SaveLoadIO saveSystem = new SaveLoadIO(true);       
    }

    private void LoseGame()
    {
        loseGameScreen.SetActive(true);
        GameStateManager.instance.SetGameState(GameState.LostByFlorp);
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
