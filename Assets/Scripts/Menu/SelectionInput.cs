using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SelectionInput : MonoBehaviour
{
    public Button playButton;
    public PlayerController controller;
    //public Player player;
    //public int playerId = 0;
    CharacterCardGenerator card;

    bool selectInput, playInput;

    // Use this for initialization
    void Start()
    {
        playButton = GameObject.FindGameObjectWithTag("PlayButton(CharSelect)").GetComponent<Button>();
        card = GetComponent<CharacterCardGenerator>();
        controller = FindObjectOfType<PlayerController>();
        //player = ReInput.players.GetPlayer(playerId);
        GetComponent<CharacterCardGenerator>().GenerateCard(controller.playerId);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        //selectInput = player.GetButtonDown("PickUp");
        //playInput = player.GetButtonDown("Interact");

        //if (selectInput)
        //{
        //    card.GenerateCard(playerId);
        //}

        //if (playInput)
        //{
        //    playButton.onClick.Invoke();
        //}

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