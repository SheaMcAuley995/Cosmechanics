using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignPlayers : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    public CharacterCardGenerator[] cards;
    public JoinGame[] joinedStatus;
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
    bool halfReady, allReady;
    int numOfPlayersReady;
    float time = 10;
    bool countingDown;


    void Start()
    {
        ExampleGameController.instance.setSpawnPoints();
        ExampleGameController.instance.numberOfPlayers = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject tempPlayer = ExampleGameController.instance.addPlayer();
        }

        // Assigns each player a spawn position
        characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos1;
        characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos2;
        characterCards[2].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos3;
        characterCards[3].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos4;

        playerControllers = FindObjectsOfType<PlayerController>();

        foreach (PlayerController controller in playerControllers)
        {
            controller.playerId = currentPlayerId;
            currentPlayerId++;
        }

        halfReady = false;
        allReady = false;
        countdownToStartText.enabled = false;
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

            // For joining the game
            if (controller.readyUp && !joinedStatus[controller.playerId].isJoined && !joinedStatus[controller.playerId].selecting)
            {
                joinedStatus[controller.playerId].selecting = true;
                StartCoroutine(joinedStatus[controller.playerId].SelectionDelay());

                joinedStatus[controller.playerId].CreateAndAssignPlayer(controller.playerId);
                ExampleGameController.instance.numberOfPlayers++;
            }

            // Player moves analog stick RIGHT - selects either a new model or an entirely new card depending on which bool you have checked
            if (controller.selectModel.x > 0 && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
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
            if (controller.selectModel.x < 0 && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
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
            if (controller.selectColourRight && !cards[controller.playerId].selecting && multipleButtonsForCustomization && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].GenerateColour();
            }

            //Player presses the left controller bumper -selects the previous colour if that setting is enabled
            if (controller.selectColourLeft && !cards[controller.playerId].selecting && multipleButtonsForCustomization && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].GeneratePreviousColour();
            }

            // Player presses left action button - selects a new crime / sentence if that setting is enabled
            if (controller.selectCrime && !cards[controller.playerId].selecting && multipleButtonsForCustomization && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].GenerateCrime();
            }

            // Player presses top action button - selects the previous crime / sentence if that setting is enabled
            if (controller.previousCrime && !cards[controller.playerId].selecting && multipleButtonsForCustomization && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].GeneratePreviousCrime();
            }

            // Player presses A - advances character status to READY
            if (controller.readyUp && !cards[controller.playerId].selecting && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.READY;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[1];

                numOfPlayersReady = 0;
                for (int i = 0; i < ExampleGameController.instance.numberOfPlayers; i++)
                {
                    if (cards[i].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
                    {
                        numOfPlayersReady++;
                    }

                    if (numOfPlayersReady >= ExampleGameController.instance.numberOfPlayers / 2)
                    {
                        halfReady = true;
                    }

                    if (numOfPlayersReady >= ExampleGameController.instance.numberOfPlayers)
                    {
                        allReady = true;
                    }
                }

                if (halfReady && !countingDown)
                {
                    countdownToStartText.enabled = true;
                    time = 10f;
                    countdown = StartCoroutine(CountdownToGame());
                }

                if (allReady && countingDown)
                {
                    time = 3f;
                }
            }

            // Player presses B - reverts character status to previous state
            if (controller.cancel && !cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.SELECTING;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[0];

                numOfPlayersReady--;
                StopCoroutine(countdown);
                countingDown = false;
                halfReady = false;
                allReady = false;
                countdownToStartText.enabled = false;
                time = 10;
            }
        }
    }

    IEnumerator CountdownToGame()
    {
        countingDown = true;
        countdownToStartText.enabled = true;

        while (true)
        {
            if (time > 0f)
            {
                time -= 1f;
                countdownToStartText.text = "Starting Game In: " + Mathf.RoundToInt(time).ToString();

                yield return new WaitForSeconds(1f);
            }
            else
            {
                PlayerActivation.instance.ContinueToGame();
                countingDown = false;
                break;
            }
        }
    }
}
