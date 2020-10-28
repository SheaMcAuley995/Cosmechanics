using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptorTutorial : MonoBehaviour {

    public float florpTotal;

    AudioSource emptySound;

    MaterialPropertyBlock propertyBlock;

    public GameObject[] FlorpFillUI;
    public bool CR_Running;
    private float florpMax;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    public void fillFlorp(float amount)
    {

        if (florpTotal < florpMax)
        {
            florpTotal += amount;
            FlorpFillUI[(int)florpTotal - 1].SetActive(true);
        }
    }


}
