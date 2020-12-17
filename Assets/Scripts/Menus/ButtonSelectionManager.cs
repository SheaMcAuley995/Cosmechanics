using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class ButtonSelectionManager : MonoBehaviour
{
    int playerID = 0;
    Player[] players = new Player[4];

    PlayerController[] controlers;
    ShipController shipController;

    Image selector, lastSelector, buttonImage, lastButtonImage;

    public List<Button> menuButtons = new List<Button>();
    [Space]
    public List<Image> buttonSelectors = new List<Image>();
    [Space]
    public List<Sprite> highlightSprites = new List<Sprite>();

    int selectedButtonIndex, lastSelectedButton;

    bool selecting, ableToGetInput;
    string currentScene;


    IEnumerator Start()
    {
        players[0] = ReInput.players.GetPlayer(playerID);
        players[1] = ReInput.players.GetPlayer(playerID + 1);
        players[2] = ReInput.players.GetPlayer(playerID + 2);
        players[3] = ReInput.players.GetPlayer(playerID + 3);

        ableToGetInput = false;
        selectedButtonIndex = -1;
        yield return new WaitForSeconds(0.2f);
        controlers = FindObjectsOfType<PlayerController>();
        //shipController = FindObjectOfType<ShipController>();
        ableToGetInput = true;
        currentScene = SceneManager.GetActiveScene().name;
        SelectButtonDownward();
    }

    void Update()
    {
        foreach (Player player in players)
        {
            if (player.GetButtonDown("Jump"))
            {
                PressButton();
            }
        }

        if (ableToGetInput && currentScene != "LevelSelectUpdated")
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
        lastButtonImage = menuButtons[lastSelectedButton].GetComponent<Image>();
        buttonImage = menuButtons[selectedButtonIndex].GetComponent<Image>();

        if (lastSelector != null)
        {
            lastSelector.enabled = false;
        }

        if (selector != null)
        {
            selector.enabled = true;
        }

        if (lastButtonImage != null)
        {
            lastButtonImage.sprite = highlightSprites[0];
        }

        if (buttonImage != null)
        {
            buttonImage.sprite = highlightSprites[1];
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
        lastButtonImage = menuButtons[lastSelectedButton].GetComponent<Image>();
        buttonImage = menuButtons[selectedButtonIndex].GetComponent<Image>();

        if (lastSelector != null)
        {
            lastSelector.enabled = false;
        }

        if (selector != null)
        {
            selector.enabled = true;
        }

        if (lastButtonImage != null)
        {
            lastButtonImage.sprite = highlightSprites[0];
        }

        if (buttonImage != null)
        {
            buttonImage.sprite = highlightSprites[1];
        }
    }

    void PressButton()
    {
        if (menuButtons[selectedButtonIndex].interactable && !selecting)
        {
            //selecting = true;
            //StartCoroutine(WaitForNextSelection());

            menuButtons[selectedButtonIndex].GetComponent<Image>().sprite = highlightSprites[2];
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
