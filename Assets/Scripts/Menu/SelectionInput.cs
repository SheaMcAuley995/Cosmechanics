using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SelectionInput : MonoBehaviour
{
    public Button playButton;
    public int playerId;
    Player player;
    CharacterCardGenerator card;
    bool interact, playInteract;

    // Use this for initialization
    void Start()
    {
        playButton = GameObject.FindGameObjectWithTag("PlayButton(CharSelect)").GetComponent<Button>();
        player = ReInput.players.GetPlayer(playerId);
        card = GetComponent<CharacterCardGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        interact = player.GetButtonDown("PickUp");
        playInteract = player.GetButtonDown("Interact");
    }

    void ProcessInput()
    {
        if (interact)
        {
            card.GenerateCard();
        }

        if (playInteract)
        {
            playButton.onClick.Invoke();
        }
    }
}