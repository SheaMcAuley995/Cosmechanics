using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SelectionInput : MonoBehaviour
{
    public int playerId;
    Player player;
    CharacterCardGenerator card;
    bool interact;

    // Use this for initialization
    void Start()
    {
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
        interact = player.GetButtonDown("Interact");
    }

    void ProcessInput()
    {
        if (interact)
        {
            card.GenerateCard();
        }
    }
}