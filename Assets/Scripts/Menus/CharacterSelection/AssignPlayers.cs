using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignPlayers : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    public CharacterCardGenerator[] cards;

    public PlayerController[] playerControllers;
    int currentPlayerId = 0;

    public Button playButton;

    int numPlayers;
    int playersReady;

    Vector3 spawnPos1 = new Vector3(-450f, 0.1725311f, 75.67999f);
    Vector3 spawnPos2 = new Vector3(-445f, 0.1725311f, 75.67999f);
    Vector3 spawnPos3 = new Vector3(-440f, 0.1725311f, 75.67999f);
    Vector3 spawnPos4 = new Vector3(-435f, 0.1725311f, 75.67999f);


    void Awake()
    {
        ExampleGameController.instance.setSpawnPoints();
        for (int i = 0; i < ExampleGameController.instance.numberOfPlayers; i++)
        {
            // Creates temporary player controllers so that each player can start with a pre-generated character
            GameObject tempPlayer = ExampleGameController.instance.addPlayer();
            // Destroys the temporary player controllers, as new ones are created upon first character generation
            Destroy(tempPlayer, 1f);
        }

        // Finds the play button
        playButton = FindObjectOfType<Button>();

        CreateAndFindCards();
    }

    // Creates the first character cards and assigns controllers to them
    void CreateAndFindCards()
    {
        // Creates first cards based on number of players
        for (int totalCharCards = 0; totalCharCards < ExampleGameController.instance.numberOfPlayers; totalCharCards++)
        {
            GameObject newCharCard = Instantiate(characterCardPrefab, panelParent.transform);
            ExampleGameController.instance.setSpawnPoints();       
        }

        // Assigns each card to array, gets their Generator component, and assigns each player a spawn position
        characterCards = GameObject.FindGameObjectsWithTag("CharacterCard");
        #region This is really really bad I'm so sorry please don't judge me too hard
        // Assigns each player a spawn position
        switch (characterCards.Length)
        {
            case 1:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos1;
                break;
            case 2:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos1;
                characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos2;
                break;
            case 3:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos1;
                characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos2;
                characterCards[2].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos3;
                break;
            case 4:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos1;
                characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos2;
                characterCards[2].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos3;
                characterCards[3].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos4;
                break;
        }
        #endregion

        cards = FindObjectsOfType<CharacterCardGenerator>();

        // Assigns each player a playerId and generates their first character
        playerControllers = FindObjectsOfType<PlayerController>();
        foreach (PlayerController playerController in playerControllers)
        {
            playerController.playerId = currentPlayerId;
            currentPlayerId++;
            cards[playerController.playerId].GenerateFullCard(playerController.playerId);
        }
        // Finds the new player controllers
        playerControllers = FindObjectsOfType<PlayerController>();
        numPlayers = cards.Length;
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        foreach (PlayerController controller in playerControllers)
        {
            controller.getInput();

            // Player moves analog stick RIGHT - selects a new head, colour, or crime/sentence for player depending on its creation status
            if (controller.movementVector.x > 0 && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].WaitForNextSelection());

                cards[controller.playerId].GenerateModel(controller.playerId);
            }

            if (controller.bumper && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].GenerateColour();
            }

            if (controller.Interact && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].GenerateCrime();
            }

            // Player presses A - advances character status to next state
            if (controller.pickUp && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].WaitForNextSelection());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.READY;
                playersReady++;

                if (playersReady == numPlayers)
                {
                    playButton.onClick.Invoke();
                }
            }

            // Player presses B - reverts character status to previous state
            if (controller.sprint && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].WaitForNextSelection());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.SELECTING_MODEL;
                playersReady--;
            }


            // TODO: Card saving & reloading
        }
    }
}
