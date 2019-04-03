using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SelectionInput : MonoBehaviour
{
    public Button playButton;
    public PlayerController controller;
    public int playerId = 0;
    CharacterCardGenerator card;

    // Use this for initialization
    void Start()
    {
        playButton = GameObject.FindGameObjectWithTag("PlayButton(CharSelect)").GetComponent<Button>();
        card = GetComponent<CharacterCardGenerator>();

        gameObject.AddComponent<PlayerController>();
        controller = GetComponent<PlayerController>();
        GetComponent<CharacterCardGenerator>().GenerateCard(playerId);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        controller.getInput();

        if (controller.pickUp)
        {
            card.GenerateCard(controller.playerId);
        }

        if (controller.Interact)
        {
            playButton.onClick.Invoke();
        }
    }
}