using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SelectionInput : MonoBehaviour
{
    public int playerId;
    Player players;

    // Use this for initialization
    void Start()
    {
        players = ReInput.players.GetPlayer(playerId);
    }

    // Update is called once per frame
    void Update()
    {
        if (players.GetButtonDown("Interact"))
        {
            GetComponent<CharacterCardGenerator>().GenerateCard();
        }
    }
}
