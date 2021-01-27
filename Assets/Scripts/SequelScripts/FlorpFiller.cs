using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpFiller : MonoBehaviour
{
    public Florp florp;

    public FlorpButton curButton;
    public FlorpButton buttonA;
    [SerializeField] GameObject buttonATutorialUI;
    public FlorpButton buttonB;
    [SerializeField] GameObject buttonBTutorialUI;

    public Transform holdPostion;

    public Material buttonOnMat;
    public Material buttonOffMat;

    [SerializeField] GameObject tutorialUI;
    [SerializeField] GameObject tutorialUIArrow;
    [SerializeField] bool isTutorial;


    private void Start()
    {
        curButton = buttonA;
        //curButton.On = true;
    }

    public void fillFlorp()
    {
        if (florp != null)
        {
            AudioEventManager.instance.PlaySound("Florp Container fill");
            if (curButton == buttonA)
            {
                florp.fillFlorp();
                buttonA.meshRenderer.material = buttonOffMat;
                buttonB.meshRenderer.material = buttonOnMat;
                curButton.On = false;
                curButton = buttonB;
                curButton.On = true;
                
                if(isTutorial)
                {
                    buttonATutorialUI.SetActive(true);
                    buttonBTutorialUI.SetActive(false);
                }
                
                if (florp.florpFillAmount == florp.florpFillMax)
                {
                    ejectFlorp();
                    florp.transform.position += florp.transform.forward;
                    florp.rb.velocity = (florp.transform.forward).normalized * 10;
                    florp = null;
                }

                
            }
            else if (curButton == buttonB)
            {
                florp.fillFlorp();
                buttonA.meshRenderer.material = buttonOnMat;
                buttonB.meshRenderer.material = buttonOffMat;
                curButton.On = false;
                curButton = buttonA;
                curButton.On = true;

                if(isTutorial)
                {
                    buttonATutorialUI.SetActive(false);
                    buttonBTutorialUI.SetActive(true);
                }

                if (florp.florpFillAmount == florp.florpFillMax)
                {
                    ejectFlorp();
                    florp.transform.position += florp.transform.forward;
                    florp.rb.velocity = (florp.transform.forward).normalized * 10;
                    florp = null;
                }
            }
        }
    }


    public void ejectFlorp()
    {
        buttonA.meshRenderer.material = buttonOffMat;
        buttonB.meshRenderer.material = buttonOffMat;
        AudioEventManager.instance.PlaySound("Florp Container eject");
        florp.rb.isKinematic = false;

        florp.FlorpFiller = null;

        if(isTutorial)
        {
            buttonATutorialUI.SetActive(false);
            buttonBTutorialUI.SetActive(false);
            tutorialUI.SetActive(false);
            tutorialUIArrow.SetActive(true);
        }

    }


}
