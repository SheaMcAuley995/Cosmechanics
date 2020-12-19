using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (florpTotal < florpMax)
        {
            florpTotal += amount;
            if(!isTutorial)
            {
                FlorpFillUI.SetInteger("FlorpSlider", (int)florpTotal);
            }
            if (isTutorial && florpTotal < florpMax)
            {
                winGameScreen.SetActive(true);
            }

        }

    }


    public IEnumerator burnFlorp()
    {
        if(!isTutorial)
        {
            while (florpTotal > florpMin)
            {
                Engine.instance.isFuled = true;
                FlorpFillUI.SetInteger("FlorpSlider", (int)florpTotal);
                florpTotal--;
                yield return new WaitForSeconds(GameplayLoopManager.TimeBetweenEvents);
            }

            Engine.instance.isFuled = false;
            CR_Running = false;
        }

    }

}
