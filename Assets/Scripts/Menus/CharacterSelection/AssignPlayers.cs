using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayers : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    public CharacterCardGenerator[] cards;

    public PlayerController[] playerControllers;
    int currentPlayerId = 0;

    Vector3 spawnPos1 = new Vector3(-450f, 0.1725311f, 75.67999f);
    Vector3 spawnPos2 = new Vector3(-445f, 0.1725311f, 75.67999f);
    Vector3 spawnPos3 = new Vector3(-440f, 0.1725311f, 75.67999f);
    Vector3 spawnPos4 = new Vector3(-435f, 0.1725311f, 75.67999f);

    [Header("Mechanic Settings")]
    public bool multipleButtonsForCustomization;
    public bool oneButtonForRandomCharacter;


    void Start()
    {
        ExampleGameController.instance.setSpawnPoints();
        for (int i = 0; i < ExampleGameController.instance.numberOfPlayers; i++)
        {
            // Creates temporary player controllers so that each player can start with a pre-generated character
            GameObject tempPlayer = ExampleGameController.instance.addPlayer();
            // Destroys the temporary player controllers, as new ones are created upon first character generation
            Destroy(tempPlayer, 1f);
        }

        CreateAndFindCards();
    }

    // Creates the first character cards and assigns controllers to them
    void CreateAndFindCards()
    {
        // Assigns each player a spawn position
        characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos1;
        characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos2;
        characterCards[2].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos3;
        characterCards[3].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos4;

        // Assigns each player a playerId and generates their first character
        playerControllers = FindObjectsOfType<PlayerController>();
        foreach (PlayerController playerController in playerControllers)
        {
            playerController.playerId = currentPlayerId;
            currentPlayerId++;
            cards[playerController.playerId].GenerateFullCard(playerController.playerId);
        }
        // Finds the new player controllers
        //playerControllers = FindObjectsOfType<PlayerController>();
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

            // Player moves analog stick RIGHT - selects either a new model or an entirely new card depending on which bool you have checked
            if (controller.movementVector.x > 0 && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                if (multipleButtonsForCustomization)
                {
                    cards[controller.playerId].GenerateModel(controller.playerId);
                }
                else if (oneButtonForRandomCharacter)
                {
                    cards[controller.playerId].GenerateFullCard(controller.playerId);
                }
            }

            // Player moves analog stick LEFT - selects either the previous card or the previous model depending on which setting is used
            if (controller.movementVector.x < 0 && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                if (multipleButtonsForCustomization)
                {
                    cards[controller.playerId].GeneratePreviousModel(controller.playerId);
                }
                else if (oneButtonForRandomCharacter)
                {
                    cards[controller.playerId].GeneratePreviousCard(controller.playerId);
                }
            }

            // Player presses the right controller bumper - selects a new colour if that setting is enabled
            if (controller.bumper && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            {
                cards[controller.playerId].GenerateColour();
            }

            // Player presses the left controller bumper - selects the previous colour if that setting is enabled
            //if (controller.leftBumper && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            //{
            //    cards[controller.playerId].GeneratePreviousColour();
            //}

            // Player presses the left action button - selects a new crime / sentence if that setting is enabled
            if (controller.Interact && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            {
                cards[controller.playerId].GenerateCrime();
            }

            // Player presses a different button(???) - selects the previous crime / sentence if that setting is enabled
            //if (controller.otherButton && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            //{
            //    cards[controller.playerId].GeneratePreviousCrime();
            //}

            // Player presses A - advances character status to READY
            if (controller.pickUp && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.READY;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[1];

                // This is pretty disgusting and I am ashamed.
                // Bad feels
                switch (playerControllers.Length)
                {
                    case 1:
                        if (cards[0].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
                        {
                            PlayerActivation.instance.ContinueToGame();
                        }
                        break;
                    case 2:
                        if (cards[0].characterStatus == CharacterCardGenerator.CharacterStatus.READY && cards[1].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
                        {
                            PlayerActivation.instance.ContinueToGame();
                        }
                        break;
                    case 3:
                        if (cards[0].characterStatus == CharacterCardGenerator.CharacterStatus.READY && cards[1].characterStatus == CharacterCardGenerator.CharacterStatus.READY && cards[2].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
                        {
                            PlayerActivation.instance.ContinueToGame();
                        }
                        break;
                    case 4:
                        if (cards[0].characterStatus == CharacterCardGenerator.CharacterStatus.READY && cards[1].characterStatus == CharacterCardGenerator.CharacterStatus.READY && cards[2].characterStatus == CharacterCardGenerator.CharacterStatus.READY && cards[3].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
                        {
                            PlayerActivation.instance.ContinueToGame();
                        }
                        break;
                }
            }

            // Player presses B - reverts character status to previous state
            if (controller.sprint && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.SELECTING;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[0];
            }
        }
    }
}
