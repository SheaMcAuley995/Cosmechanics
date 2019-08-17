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
                StartCoroutine(joinedStatus[controller.playerId].SelectionDelay());

                cards[controller.playerId].DeactivateJoinIcons();

                joinedStatus[controller.playerId].CreateAndAssignPlayer(controller.playerId);
                ExampleGameController.instance.numberOfPlayers++;
            }

            // For un-joining the game
            if (controller.Button_B && joinedStatus[controller.playerId].isJoined && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY)
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
            if (controller.Button_RB && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].NextColour();
            }

            //Player presses the left controller bumper - selects the previous colour
            if (controller.Button_LB && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].PreviousColour();
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
