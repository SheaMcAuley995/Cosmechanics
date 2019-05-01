using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerJoin : MonoBehaviour
{
    public TextMeshProUGUI playersJoined;
    public PlayerController[] controllers;
    bool selecting;

	IEnumerator Start ()
    {
        ExampleGameController.instance.numberOfPlayers = 1;
        playersJoined.text = ExampleGameController.instance.numberOfPlayers.ToString();

        yield return new WaitForEndOfFrame();
        controllers = FindObjectsOfType<PlayerController>();
    }
	
	void Update ()
    {
        GetInput();
	}

    void GetInput()
    {
        foreach(PlayerController controller in controllers)
        {
            controller.getInput();

            if (controller.movementVector.x > 0 && !selecting)
            {
                selecting = true;
                StartCoroutine(WaitForNextSelect());

                if (ExampleGameController.instance.numberOfPlayers < 4)
                {
                    ExampleGameController.instance.numberOfPlayers++;
                    ExampleGameController.instance.setSpawnPoints();
                }
                else
                {
                    ExampleGameController.instance.numberOfPlayers = 1;
                    ExampleGameController.instance.setSpawnPoints();
                }

                playersJoined.text = ExampleGameController.instance.numberOfPlayers.ToString();
            }

            if (controller.movementVector.x < 0 && !selecting)
            {
                selecting = true;
                StartCoroutine(WaitForNextSelect());

                if (ExampleGameController.instance.numberOfPlayers > 1)
                {
                    ExampleGameController.instance.numberOfPlayers--;
                    ExampleGameController.instance.setSpawnPoints();
                }
                else
                {
                    ExampleGameController.instance.numberOfPlayers = 4;
                    ExampleGameController.instance.setSpawnPoints();
                }

                playersJoined.text = ExampleGameController.instance.numberOfPlayers.ToString();
            }
        }
    }

    IEnumerator WaitForNextSelect()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }
}
