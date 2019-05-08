using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectionManager : MonoBehaviour
{
    PlayerController[] controlers;
    Animator animator, lastAnimator;
    Image selector, lastSelector;

    public List<Button> menuButtons = new List<Button>();
    [Space]
    public List<Image> buttonSelectors = new List<Image>();

    int selectedButtonIndex, lastSelectedButton;
    bool selecting, ableToGetInput;


    IEnumerator Start()
    {
        ableToGetInput = false;
        selectedButtonIndex = -1;
        SelectButtonDownward();
        yield return new WaitForSeconds(0.2f);
        controlers = FindObjectsOfType<PlayerController>();
        ableToGetInput = true;
    }

    void Update()
    {
        if (ableToGetInput)
        {
            foreach (PlayerController controler in controlers)
            {
                controler.getInput();

                if (controler.movementVector.y < 0 && !selecting)
                {
                    SelectButtonDownward();
                }

                if (controler.movementVector.y > 0 && !selecting)
                {
                    SelectButtonUpward();
                }

                if (controler.pickUp && !selecting)
                {
                    PressButton();
                }
            }
        }
    }

    void SelectButtonDownward()
    {
        selecting = true;
        StartCoroutine(WaitForNextSelection());

        selectedButtonIndex++;
        if (selectedButtonIndex > buttonSelectors.Count - 1)
        {
            selectedButtonIndex = 0;
        }
        lastSelectedButton = selectedButtonIndex - 1;
        if (lastSelectedButton < 0)
        {
            lastSelectedButton = buttonSelectors.Count - 1;
        }

        lastSelector = buttonSelectors[lastSelectedButton].GetComponent<Image>();
        selector = buttonSelectors[selectedButtonIndex].GetComponent<Image>();

        if (lastSelector != null)
        {
            lastSelector.enabled = false;
        }

        if (selector != null)
        {
            selector.enabled = true;
        }
    }

    void SelectButtonUpward()
    {
        selecting = true;
        StartCoroutine(WaitForNextSelection());

        selectedButtonIndex--;
        if (selectedButtonIndex < 0)
        {
            selectedButtonIndex = buttonSelectors.Count - 1;
        }
        lastSelectedButton = selectedButtonIndex + 1;
        if (lastSelectedButton > buttonSelectors.Count - 1)
        {
            lastSelectedButton = 0;
        }

        lastSelector = buttonSelectors[lastSelectedButton].GetComponent<Image>();
        selector = buttonSelectors[selectedButtonIndex].GetComponent<Image>();

        if (lastSelector != null)
        {
            lastSelector.enabled = false;
        }

        if (selector != null)
        {
            selector.enabled = true;
        }
    }

    void PressButton()
    {
        if (menuButtons[selectedButtonIndex].interactable && !selecting)
        {
            //selecting = true;
            //StartCoroutine(WaitForNextSelection());

            menuButtons[selectedButtonIndex].onClick.Invoke();
        }
    }

    IEnumerator WaitForNextSelection()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }
}
