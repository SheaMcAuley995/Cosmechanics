using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerJoin : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    public Button backButton;
    public TextMeshProUGUI playersJoined;
    public PlayerController[] controllers;
    bool selecting;
    bool optionsOpen;

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

            if (controller.pickUp && !optionsOpen)
            {
                for (int i = 0; i < controllers.Length; i++)
                {
                    Destroy(controllers[i]);
                }
                playButton.onClick.Invoke();
            }

            if (controller.Interact && !optionsOpen && !selecting)
            {
                StartCoroutine(WaitForNextSelect());
                optionsOpen = true;
                optionsButton.onClick.Invoke();
            }

            if (controller.sprint && !optionsOpen && !selecting)
            {
                StartCoroutine(WaitForNextSelect());
                exitButton.onClick.Invoke();
            }

            if (controller.sprint && optionsOpen && !selecting)
            {
                StartCoroutine(WaitForNextSelect());
                optionsOpen = false;
                backButton.onClick.Invoke();
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
