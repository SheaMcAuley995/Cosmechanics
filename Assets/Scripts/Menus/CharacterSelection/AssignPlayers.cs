using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignPlayers : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    public CharacterCardGenerator[] cards;
    public TextMeshProUGUI countdownToStartText;

    public PlayerController[] playerControllers;
    int currentPlayerId = 0;

    Vector3 spawnPos1 = new Vector3(-450f, 0.1725311f, 75.67999f);
    Vector3 spawnPos2 = new Vector3(-445f, 0.1725311f, 75.67999f);
    Vector3 spawnPos3 = new Vector3(-440f, 0.1725311f, 75.67999f);
    Vector3 spawnPos4 = new Vector3(-435f, 0.1725311f, 75.67999f);

    [Header("Mechanic Settings")]
    public bool multipleButtonsForCustomization;
    public bool oneButtonForRandomCharacter;
    bool allReady;


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
            if (controller.selectModel.x > 0 && !cards[controller.playerId].selecting)
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
            if (controller.selectModel.x < 0 && !cards[controller.playerId].selecting)
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
            if (controller.selectColourRight && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            {
                cards[controller.playerId].GenerateColour();
            }

            //Player presses the left controller bumper -selects the previous colour if that setting is enabled
            if (controller.selectColourLeft && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            {
                cards[controller.playerId].GeneratePreviousColour();
            }

            // Player presses left action button - selects a new crime / sentence if that setting is enabled
            if (controller.selectCrime && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            {
                cards[controller.playerId].GenerateCrime();
            }

            // Player presses top action button - selects the previous crime / sentence if that setting is enabled
            if (controller.previousCrime && !cards[controller.playerId].selecting && multipleButtonsForCustomization)
            {
                cards[controller.playerId].GeneratePreviousCrime();
            }

            // Player presses A - advances character status to READY
            if (controller.readyUp && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());
                
                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.READY;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[1];

                // This is how the code was always written what other version I don't know what you're talking about
                // It definitely wasn't a giant, awful switch statement nope no sir 
                for (int i = 0; i < ExampleGameController.instance.numberOfPlayers; i++)
                {
                    if (cards[i].characterStatus == CharacterCardGenerator.CharacterStatus.SELECTING)
                    {
                        allReady = false;
                        break;
                    }
                    allReady = true;
                }

                if (allReady)
                {
                    PlayerActivation.instance.ContinueToGame();
                }
            }

            // Player presses B - reverts character status to previous state
            if (controller.cancel && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.SELECTING;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[0];
            }
        }
    }
}
