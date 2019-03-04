using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SelectionManager : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    [HideInInspector] public int playerId = 0;
    Player[] gamePlayers;
    bool[] playerInput; 

	void Awake ()
    {
        CreateAndFindCards();
        AssignPlayerIDsAndInput();
    }

    #region Awake Methods
    void CreateAndFindCards()
    {
        for (int totalCharCards = 0; totalCharCards < ExampleGameController.instance.numberOfPlayers; totalCharCards++)
        {
            GameObject newCharCard = Instantiate(characterCardPrefab, panelParent.transform);
            newCharCard.GetComponent<CharacterCardGenerator>().GenerateCard();
        }
        characterCards = GameObject.FindGameObjectsWithTag("CharacterCard");
    }
    void AssignPlayerIDsAndInput()
    {
        gamePlayers = new Player[ExampleGameController.instance.numberOfPlayers];
        for (int playerNumber = 0; playerNumber < ExampleGameController.instance.numberOfPlayers; playerNumber++)
        {
            gamePlayers[playerNumber] = ReInput.players.GetPlayer(playerId);
        }
        playerInput = new bool[ExampleGameController.instance.numberOfPlayers];
    }
    #endregion

    void Update()
    {
        for (int playerNum = 0; playerNum < ExampleGameController.instance.numberOfPlayers; playerNum++)
        {
            playerInput[playerNum] = gamePlayers[playerNum].GetButtonDown("Interact");

            if (playerInput[playerNum])
            {
                characterCards[playerNum].GetComponent<CharacterCardGenerator>().GenerateCard();
            }
        }
    }
}
