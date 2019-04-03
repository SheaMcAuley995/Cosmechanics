using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerJoin : MonoBehaviour
{
    public Button playButton;
    public TextMeshProUGUI playersJoined;
    public PlayerController[] controllers;

	IEnumerator Start ()
    {
        ExampleGameController.instance.numberOfPlayers = 2;
        playersJoined.text = ExampleGameController.instance.numberOfPlayers.ToString();

        yield return new WaitForSeconds(1f);
        controllers = GameObject.FindObjectsOfType<PlayerController>();
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

            if (controller.pickUp)
            {
                if (ExampleGameController.instance.numberOfPlayers < 4)
                {
                    ExampleGameController.instance.numberOfPlayers++;
                    ExampleGameController.instance.setSpawnPoints();
                }
                else
                {
                    ExampleGameController.instance.numberOfPlayers = 2;
                    ExampleGameController.instance.setSpawnPoints();
                }

                playersJoined.text = ExampleGameController.instance.numberOfPlayers.ToString();
            }

            if (controller.Interact)
            {
                playButton.onClick.Invoke();
            }
        }
    }
}
