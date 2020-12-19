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
        CharacterHandler.instance.numberOfPlayers = 1;
        playersJoined.text = CharacterHandler.instance.numberOfPlayers.ToString();

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
    
                if (CharacterHandler.instance.numberOfPlayers < 4)
                {
                    CharacterHandler.instance.numberOfPlayers++;
                    CharacterHandler.instance.SetSpawnPoints();
                }
                else
                {
                    CharacterHandler.instance.numberOfPlayers = 1;
                    CharacterHandler.instance.SetSpawnPoints();
                }
    
                playersJoined.text = CharacterHandler.instance.numberOfPlayers.ToString();
            }
    
            if (controller.movementVector.x < 0 && !selecting)
            {
                selecting = true;
                StartCoroutine(WaitForNextSelect());
    
                if (CharacterHandler.instance.numberOfPlayers > 1)
                {
                    CharacterHandler.instance.numberOfPlayers--;
                    CharacterHandler.instance.SetSpawnPoints();
                }
                else
                {
                    CharacterHandler.instance.numberOfPlayers = 4;
                    CharacterHandler.instance.SetSpawnPoints();
                }
    
                playersJoined.text = CharacterHandler.instance.numberOfPlayers.ToString();
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
