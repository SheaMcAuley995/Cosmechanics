using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignPlayers : MonoBehaviour
{
    public CharacterCardGenerator[] cards;
    public JoinGame[] joinedStatus;

    public PlayerController[] controllers;
    int playerId = 0;
    int currentPlayerId = 0;

    [Header("Player Colours")]
    public List<Material> availableColors = new List<Material>();
    public List<Material> takenColors = new List<Material>();

    [Header("Spawn Positions -- DON'T TOUCH THESE PLEASE!!")]
    [SerializeField] Vector3[] spawnPositions = new Vector3[4];

    bool checkingInput = false;


    IEnumerator Start()
    {
        ExampleGameController.instance.setSpawnPoints();
        ExampleGameController.instance.numberOfPlayers = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject tempPlayer = ExampleGameController.instance.addPlayer();

            // Assigns each player a spawn position.
            cards[i].gameObject.GetComponent<CharacterCardGenerator>().spawnPos = spawnPositions[i];
        }
        controllers = FindObjectsOfType<PlayerController>();

        foreach (PlayerController controller in controllers)
        {
            controller.playerId = currentPlayerId;
            currentPlayerId++;
        }

        yield return new WaitForSeconds(0.2f);
        checkingInput = true;
    }

    void Update()
    {
        if (checkingInput)
            GetInput();
    }

    void GetInput()
    {
        foreach (PlayerController controller in controllers)
        {
            // For joining the game
            if (controller.Button_A && !joinedStatus[controller.playerId].isJoined && !joinedStatus[controller.playerId].selecting)
            {
                joinedStatus[controller.playerId].selecting = true;
                cards[controller.playerId].selecting = true;
                StartCoroutine(joinedStatus[controller.playerId].SelectionDelay());

                cards[controller.playerId].DeactivateJoinIcons();

                joinedStatus[controller.playerId].CreateAndAssignPlayer(controller.playerId);
                ExampleGameController.instance.numberOfPlayers++;

                //If a player joins during the countdown, stop the countdown
                if (!ReadyCheck.instance.AllPlayersReady())
                {
                    ReadyCheck.instance.StopCountdown();
                }
            }

            //For un-joining the game --DISABLING UNTIL I CAN FIGURE OUT HOW TO HANDLE COLOR REMOVAL
            if (controller.Button_B && joinedStatus[controller.playerId].isJoined && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY)
            {
                // Adds the character's color to the list of available colours, and removes it from the list of taken colours
                availableColors.Add(takenColors[controller.playerId]);
                //takenColors.RemoveAt(controller.playerId);
                takenColors[controller.playerId] = null;
                //this doesn't work because if say player 2 tries to un-join but player 1 has already un-joined, the index is OOA. 

                cards[controller.playerId].ActivateJoinIcons();
                joinedStatus[controller.playerId].UnjoinGame(controller.playerId);
                ExampleGameController.instance.numberOfPlayers--;

                // If a player leaves and everyone else is ready, start the countdown
                if (ReadyCheck.instance.AllPlayersReady())
                {
                    ReadyCheck.instance.StartCountdown();
                }
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

            // Player presses A - advances character status to READY
            if (controller.Button_A && !joinedStatus[controller.playerId].selecting && joinedStatus[controller.playerId].isJoined && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.READY;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[1];

                ReadyCheck.instance.IncreasePlayersReady();
            }

            //Player presses B - reverts character status to previous state
            if (controller.sprint && !cards[controller.playerId].selecting && joinedStatus[controller.playerId].isJoined && cards[controller.playerId].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());

                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.SELECTING;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[0];

                ReadyCheck.instance.DecreasePlayersReady();
            }
        }
    }
}
