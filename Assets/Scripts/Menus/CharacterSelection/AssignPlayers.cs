using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignPlayers : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    public CharacterCardGenerator[] cards;
    public JoinGame[] joinedStatus;
    public Text countdownToStartText;

    public PlayerController[] controllers;
    SelectedPlayer[] players;
    int currentPlayerId = 0;

    Vector3 spawnPos1 = new Vector3(-450f, 0.1725311f, 75.67999f);
    Vector3 spawnPos2 = new Vector3(-445f, 0.1725311f, 75.67999f);
    Vector3 spawnPos3 = new Vector3(-440f, 0.1725311f, 75.67999f);
    Vector3 spawnPos4 = new Vector3(-435f, 0.1725311f, 75.67999f);

    [Header("Mechanic Settings")]
    bool halfReady, allReady;
    int numOfPlayersReady;
    float time = 10;
    bool countingDown;
    int playerId = 0;

    Coroutine countdown;


    void Start()
    {
        ExampleGameController.instance.setSpawnPoints();
        ExampleGameController.instance.numberOfPlayers = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject tempPlayer = ExampleGameController.instance.addPlayer();
        }
        controllers = FindObjectsOfType<PlayerController>();

        // Assigns each player a spawn position
        characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos1;
        characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos2;
        characterCards[2].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos3;
        characterCards[3].GetComponent<CharacterCardGenerator>().spawnPos = spawnPos4;

        foreach (PlayerController controller in controllers)
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
        foreach (PlayerController controller in controllers)
        {
            controller.getInput();

            // For joining the game
            if (controller.readyUp && !joinedStatus[controller.playerId].isJoined && !joinedStatus[controller.playerId].selecting)
            {
                joinedStatus[controller.playerId].selecting = true;
                StartCoroutine(joinedStatus[controller.playerId].SelectionDelay());

                cards[controller.playerId].DeactivateJoinIcons();

                joinedStatus[controller.playerId].CreateAndAssignPlayer(controller.playerId);
                ExampleGameController.instance.numberOfPlayers++;
                players = FindObjectsOfType<SelectedPlayer>();
            }

            // For un-joining the game
            if (controller.cancel && joinedStatus[controller.playerId].isJoined && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].ActivateJoinIcons();
                joinedStatus[controller.playerId].UnjoinGame(controller.playerId);
                ExampleGameController.instance.numberOfPlayers--;
            }

            // Player moves analog stick RIGHT - selects a new head
            if (controller.selectModel.x > 0 && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].selecting = true;
                cards[controller.playerId].NextHead();
            }

            // Player moves analog stick LEFT - selects the previous head
            if (controller.selectModel.x < 0 && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].selecting = true;
                cards[controller.playerId].PreviousHead();
            }

            // Turns off 'selecting' when the analog stick returns to 0
            if (controller.selectModel.x == 0 && cards[controller.playerId].selecting)
            {
                cards[controller.playerId].selecting = false;
            }

            // Player presses the right controller bumper - selects the next colour
            if (controller.selectColourRight && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].NextColour();
            }

            //Player presses the left controller bumper - selects the previous colour
            if (controller.selectColourLeft && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].PreviousColour();
            }

            // Player presses A - advances character status to READY
            if (controller.readyUp && !joinedStatus[controller.playerId].selecting && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.READY;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[1];

                numOfPlayersReady = 0;
                for (int i = 0; i < cards.Length; i++)
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

                if (allReady)
                {
                    PlayerActivation.instance.ContinueToGame();
                }
            }

            // This is no longer needed because I'm not being a dumb dumb anymore :D 
            //// Player presses START - begins the countdown to start game
            //if (controller.start && !cards[controller.playerId].selecting)
            //{
            //    cards[controller.playerId].selecting = true;
            //    StartCoroutine(cards[controller.playerId].SelectionDelay());

            //    time = 4f;
            //    countdown = StartCoroutine(CountdownToGame());
            //}

            // Player presses B - reverts character status to previous state
            if (controller.cancel && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
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
                countdownToStartText.text = "Press 'START' to begin the game!";
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
