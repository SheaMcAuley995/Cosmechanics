using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlorpReceptor : MonoBehaviour
{

    public bool isTutorial;
    [SerializeField] GameObject winGameScreen;
    public float florpTotal;

    AudioSource emptySound;

    public float florpMax = 8;
    public float florpMin = 0;
    //float currentFill;
    float amountDeposited;
    float emptyTotal;

    Florp currentContainer;

    Renderer florpRenderer;
    MaterialPropertyBlock propertyBlock;

    public Animator FlorpFillUI;
    [SerializeField] GameObject shipUITutorial;
    [SerializeField] Slider ShipProgressSlider;
    [HideInInspector] public bool endTutorial = false;
    int currentProgress = 0;
    public bool CR_Running;
    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        StartCoroutine(burnFlorp());
    }

    public void fillFlorp(float amount)
    {
        if(!endTutorial)
        {
            if (florpTotal < florpMax)
            {
                AudioEventManager.instance.PlaySound("Florp receptor fill");
                florpTotal += amount;

                FlorpFillUI.SetInteger("FlorpSlider", (int)florpTotal);

                if (!isTutorial && florpTotal >= florpMax)
                {
                    winGameScreen.SetActive(true);
                    AudioEventManager.instance.StopAllSounds();
                    AudioEventManager.instance.PlaySound("Music Win 1");
                    SaveLoadIO saveSystem = new SaveLoadIO(true);
                }

                if (isTutorial && florpTotal >= florpMax)
                {
                    shipUITutorial.SetActive(true);
                    StartCoroutine(burnFlorpTutorial());
                    endTutorial = true;
                }

            }

        }

    }

    private IEnumerator burnFlorpTutorial()
    {

       
        yield return new WaitForSeconds(1);

        while (florpTotal > florpMin)
        {
            florpTotal--;

            ShipProgressSlider.value = currentProgress / florpMax;
            FlorpFillUI.SetInteger("FlorpSlider", (int)florpTotal);
            currentProgress++;
            yield return new WaitForSeconds(1);
        }

        winGameScreen.SetActive(true);
        AudioEventManager.instance.StopAllSounds();
        AudioEventManager.instance.PlaySound("Music Win 1");
        SaveLoadIO saveSystem = new SaveLoadIO(true);
    }


    public IEnumerator burnFlorp()
    {
        if(!isTutorial)
        {
            while (florpTotal > florpMin)
            {
                Engine.instance.isFueled = true;
                florpTotal--;
                FlorpFillUI.SetInteger("FlorpSlider", (int)florpTotal);
                yield return new WaitForSeconds(GameplayLoopManager.TimeBetweenEvents);
            }

            Engine.instance.isFueled = false;
            CR_Running = false;
        }

    }

}
