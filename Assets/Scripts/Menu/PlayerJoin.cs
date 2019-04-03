using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class PlayerJoin : MonoBehaviour
{
    public Button playButton;
    public TextMeshProUGUI playersJoined;
    [HideInInspector] public int playerId = 0;
    Player[] players;
    ExampleGameController controller;
    bool input, playInput;

	void Start ()
    {
        controller = ExampleGameController.instance;

        controller.numberOfPlayers = 2;
        playersJoined.text = controller.numberOfPlayers.ToString();

        players = new Player[controller.numberOfPlayers];
        for (int playerNumber = 0; playerNumber < controller.numberOfPlayers; playerNumber++)
        {
            players[playerNumber] = ReInput.players.GetPlayer(playerId);
        }
    }
	
	void Update ()
    {
        GetInput();
        ApplyInput();
	}

    void GetInput()
    {
        foreach(Player player in players)
        {
            input = player.GetButtonDown("PickUp");
            playInput = player.GetButtonDown("Interact");
        }
    }

    void ApplyInput()
    {
        if (input)
        {
            switch (controller.numberOfPlayers)
            {
                case 2:
                    controller.numberOfPlayers++;
                    controller.setSpawnPoints();
                    break;
                case 3:
                    controller.numberOfPlayers++;
                    controller.setSpawnPoints();
                    break;
                case 4:
                    controller.numberOfPlayers = 2;
                    controller.setSpawnPoints();
                    break;
            }

            playersJoined.text = controller.numberOfPlayers.ToString();
        }

        if (playInput)
        {
            playButton.onClick.Invoke();
        }
    }
}
