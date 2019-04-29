using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectionManager : MonoBehaviour
{
    PlayerController[] controlers;
    Animator animator, lastAnimator;

    public bool inMainMenu, inOverworld, inWinScreen, inLoseScreen;
    [Space]
    [Header("Buttons - Please only populate for the corresponding scenes")]
    public List<Button> mainMenuButtons = new List<Button>();
    public List<Button> overworldButtons = new List<Button>();
    public List<Button> winScreenButtons = new List<Button>();
    public List<Button> loseScreenButtons = new List<Button>();

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

        if (inMainMenu)
        {
            selectedButtonIndex++;
            if (selectedButtonIndex > mainMenuButtons.Count - 1)
            {
                selectedButtonIndex = 0;
            }
            lastSelectedButton = selectedButtonIndex - 1;
            if (lastSelectedButton < 0)
            {
                lastSelectedButton = mainMenuButtons.Count - 1;
            }

            lastAnimator = mainMenuButtons[lastSelectedButton].GetComponent<Animator>();
            animator = mainMenuButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else if (inOverworld)
        {
            selectedButtonIndex++;
            if (selectedButtonIndex > overworldButtons.Count - 1)
            {
                selectedButtonIndex = 0;
            }
            lastSelectedButton = selectedButtonIndex - 1;
            if (lastSelectedButton < 0)
            {
                lastSelectedButton = overworldButtons.Count - 1;
            }

            lastAnimator = overworldButtons[lastSelectedButton].GetComponent<Animator>();
            animator = overworldButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else if (inWinScreen)
        {
            selectedButtonIndex++;
            if (selectedButtonIndex > winScreenButtons.Count - 1)
            {
                selectedButtonIndex = 0;
            }
            lastSelectedButton = selectedButtonIndex - 1;
            if (lastSelectedButton < 0)
            {
                lastSelectedButton = winScreenButtons.Count - 1;
            }

            lastAnimator = winScreenButtons[lastSelectedButton].GetComponent<Animator>();
            animator = winScreenButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else if (inLoseScreen)
        {
            selectedButtonIndex++;
            if (selectedButtonIndex > loseScreenButtons.Count - 1)
            {
                selectedButtonIndex = 0;
            }
            lastSelectedButton = selectedButtonIndex - 1;
            if (lastSelectedButton < 0)
            {
                lastSelectedButton = loseScreenButtons.Count - 1;
            }

            lastAnimator = loseScreenButtons[lastSelectedButton].GetComponent<Animator>();
            animator = loseScreenButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("You're trying to select a button but aren't in a menu. This shouldn't be possible...");
        }

        if (lastAnimator != null)
        {
            lastAnimator.SetBool("isSelecting", false);
        }
        if (animator != null)
        {
            animator.SetBool("isSelecting", true);
        }
    }

    void SelectButtonUpward()
    {
        selecting = true;
        StartCoroutine(WaitForNextSelection());

        if (inMainMenu)
        {
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
            {
                selectedButtonIndex = mainMenuButtons.Count - 1;
            }
            lastSelectedButton = selectedButtonIndex + 1;
            if (lastSelectedButton > mainMenuButtons.Count - 1)
            {
                lastSelectedButton = 0;
            }

            lastAnimator = mainMenuButtons[lastSelectedButton].GetComponent<Animator>();
            animator = mainMenuButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else if (inOverworld)
        {
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
            {
                selectedButtonIndex = overworldButtons.Count - 1;
            }
            lastSelectedButton = selectedButtonIndex + 1;
            if (lastSelectedButton > overworldButtons.Count - 1)
            {
                lastSelectedButton = 0;
            }

            lastAnimator = overworldButtons[lastSelectedButton].GetComponent<Animator>();
            animator = overworldButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else if (inWinScreen)
        {
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
            {
                selectedButtonIndex = winScreenButtons.Count - 1;
            }
            lastSelectedButton = selectedButtonIndex + 1;
            if (lastSelectedButton > winScreenButtons.Count - 1)
            {
                lastSelectedButton = 0;
            }

            lastAnimator = winScreenButtons[lastSelectedButton].GetComponent<Animator>();
            animator = winScreenButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else if (inLoseScreen)
        {
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
            {
                selectedButtonIndex = loseScreenButtons.Count - 1;
            }
            lastSelectedButton = selectedButtonIndex + 1;
            if (lastSelectedButton > loseScreenButtons.Count - 1)
            {
                lastSelectedButton = 0;
            }

            lastAnimator = loseScreenButtons[lastSelectedButton].GetComponent<Animator>();
            animator = loseScreenButtons[selectedButtonIndex].GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("You're trying to select a button but aren't in a menu. This shouldn't be possible...");
        }

        if (lastAnimator != null)
        {
            lastAnimator.SetBool("isSelecting", false);
        }
        if (animator != null)
        {
            animator.SetBool("isSelecting", true);
        }
    }

    void PressButton()
    {
        selecting = true;
        StartCoroutine(WaitForNextSelection());

        if (inMainMenu)
        {
            mainMenuButtons[selectedButtonIndex].onClick.Invoke();
        }
        else if (inOverworld)
        {
            overworldButtons[selectedButtonIndex].onClick.Invoke();
        }
        else if (inWinScreen)
        {
            winScreenButtons[selectedButtonIndex].onClick.Invoke();
        }
        else if (inLoseScreen)
        {
            loseScreenButtons[selectedButtonIndex].onClick.Invoke();
        }
    }

    IEnumerator WaitForNextSelection()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }
}
