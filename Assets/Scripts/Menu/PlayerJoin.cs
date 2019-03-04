using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rewired;

public class PlayerJoin : MonoBehaviour
{
    public TextMeshProUGUI playersJoined;
    [HideInInspector] public int playerId = 0;
    Player[] players;
    bool input;

	void Start ()
    {
        ExampleGameController.instance.numberOfPlayers = 2;
        playersJoined.text = ExampleGameController.instance.numberOfPlayers.ToString();

        players = new Player[ExampleGameController.instance.numberOfPlayers];
        for (int playerNumber = 0; playerNumber < ExampleGameController.instance.numberOfPlayers; playerNumber++)
        {
            players[playerNumber] = ReInput.players.GetPlayer(playerId);
        }
    }
	
	void Update ()
    {
        GetInput();
	}

    void GetInput()
    {
        input = players[0].GetButtonDown("Interact");

        if (input && ExampleGameController.instance.numberOfPlayers < 4)
        {
            ExampleGameController.instance.numberOfPlayers++;
            ExampleGameController.instance.setSpawnPoints();
        }

        playersJoined.text = ExampleGameController.instance.numberOfPlayers.ToString();
    }
}
