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
    ExampleGameController controller;
    bool input;
    int numOfPlayers;

	void Start ()
    {
        controller = ExampleGameController.instance;
        numOfPlayers = controller.numberOfPlayers;

        numOfPlayers = 2;
        playersJoined.text = numOfPlayers.ToString();

        players = new Player[numOfPlayers];
        for (int playerNumber = 0; playerNumber < numOfPlayers; playerNumber++)
        {
            players[playerNumber] = ReInput.players.GetPlayer(playerId);
        }
    }
	
	void Update ()
    {
        GetInput();
        ProcessInput();
	}

    void GetInput()
    {
        input = players[0].GetButtonDown("Interact");
    }

    void ProcessInput()
    {
        if (input)
        {
            switch (numOfPlayers)
            {
                case 2:
                    numOfPlayers++;
                    controller.setSpawnPoints();
                    break;
                case 3:
                    numOfPlayers++;
                    controller.setSpawnPoints();
                    break;
                case 4:
                    numOfPlayers = 2;
                    controller.setSpawnPoints();
                    break;
            }

            playersJoined.text = numOfPlayers.ToString();
        }
    }
}
