using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptor : MonoBehaviour
{
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

    public GameObject[] FlorpFillUI;
    public bool CR_Running;
    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        emptySound = GetComponent<AudioSource>();
        //StartCoroutine(FlorpEmpty());
        //florpRenderer.GetPropertyBlock(propertyBlock);
    }

    private void Start()
    {
        StartCoroutine(burnFlorp());
    }

    public void fillFlorp(float amount)
    {

        if (florpTotal < florpMax)
        {
            Debug.Log("GLUB 2");
            florpTotal += amount;
            FlorpFillUI[(int)florpTotal - 1].SetActive(true);
        }
    }


    public IEnumerator burnFlorp()
    {
        while (florpTotal > florpMin)
        {
            Engine.instance.isFuled = true;
            FlorpFillUI[(int)florpTotal - 1].SetActive(false);
            florpTotal--;
            yield return new WaitForSeconds(GameplayLoopManager.TimeBetweenEvents);
        }

        Engine.instance.isFuled = false;
        CR_Running = false;
    }

}
