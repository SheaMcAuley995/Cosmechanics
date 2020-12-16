using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AssignPlayers : MonoBehaviour
{
    [SerializeField] string mainMenuScene;

    public CharacterCardGenerator[] cards;
    public JoinGame[] joinedStatus;
   
    public PlayerController[] controllers;
    int playerId = 0;
    int currentPlayerId = 0;

    [Header("Player Skins ")]
    public PossibleCharacters[] possibleCharacters;
   
    [Header("Spawn Positions -- DON'T TOUCH THESE PLEASE!!")]
    [SerializeField] Transform[] spawnPositions;
   
    bool checkingInput = false;
   
   
    IEnumerator Start()
    {
        CharacterHandler.instance.SetSpawnPoints();
        CharacterHandler.instance.numberOfPlayers = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject tempPlayer = CharacterHandler.instance.addPlayer(new Vector3(0, i, 0));
              
            // Assigns each player a spawn position.
            //ards[i].gameObject.GetComponent<CharacterCardGenerator>().spawnPos = spawnPositions[i];

            //tempPlayer.SetActive(false);
        }
        controllers = FindObjectsOfType<PlayerController>();
   
        foreach (PlayerController controller in controllers)
        {
            controller.playerId = currentPlayerId++;
            //currentPlayerId++;
        }
   
        yield return new WaitForSeconds(0.2f);
        checkingInput = true;
    }
   
    void Update()
    {
        if (checkingInput)
            GetInput();
    }

    // Checks if any players have joined the game
    bool AnyPlayersJoined()
    {
        for (int i = 0; i < joinedStatus.Length; i++)
        {
            if (joinedStatus[i].isJoined)
            {
                return true;
            }
        }

        return false;
    }
   
    void GetInput()
    {
        foreach (PlayerController controller in controllers)
        {
            // For going back to the main menu
            if (controller.Button_B && !joinedStatus[controller.playerId].selecting)
            {
                if (!AnyPlayersJoined())
                {
                    SceneFader.instance.FadeTo(mainMenuScene);
                }
            }

            // For joining the game
            if (controller.Button_A && !joinedStatus[controller.playerId].isJoined && !joinedStatus[controller.playerId].selecting)
            {
                joinedStatus[controller.playerId].selecting = true;
                cards[controller.playerId].selecting = true;
                StartCoroutine(joinedStatus[controller.playerId].SelectionDelay());
   
                cards[controller.playerId].DeactivateJoinIcons();
   
                joinedStatus[controller.playerId].CreateAndAssignPlayer(controller.playerId, spawnPositions[controller.playerId].position);
                CharacterHandler.instance.numberOfPlayers++;
                //CharacterHandler.instance.players[i]
                //If a player joins during the countdown, stop the countdown
                if (!ReadyCheck.instance.AllPlayersReady())
                {
                    ReadyCheck.instance.StopCountdown();
                }
            }
   
            //For un-joining the game --DISABLING UNTIL I CAN FIGURE OUT HOW TO HANDLE COLOR REMOVAL
            if (controller.Button_B && joinedStatus[controller.playerId].isJoined && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY)
            {
                joinedStatus[controller.playerId].selecting = true;
                StartCoroutine(joinedStatus[controller.playerId].SelectionDelay());

                // Adds the character's color to the list of available colours, and removes it from the list of taken colours
                //availableColors.Add(takenColors[controller.playerId]);
                //takenColors.RemoveAt(controller.playerId);
                //takenColors[controller.playerId] = null;
                //this doesn't work because if say player 2 tries to un-join but player 1 has already un-joined, the index is OOA. 
   
                cards[controller.playerId].ActivateJoinIcons();
                joinedStatus[controller.playerId].UnjoinGame(controller.playerId);
                CharacterHandler.instance.numberOfPlayers--;
   
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
                cards[controller.playerId].NextCharacter();
            }
   
            // Player moves analog stick LEFT - selects the previous head
            if (controller.selectModel.x < 0 && !cards[controller.playerId].selecting && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && joinedStatus[controller.playerId].isJoined)
            {
                cards[controller.playerId].selecting = true;
                cards[controller.playerId].PreviousCharacter();
            }
   
            // Turns off 'selecting' when the analog stick returns to 0
            //if (controller.selectModel.x == 0 && cards[controller.playerId].selecting)
            //{
            //    cards[controller.playerId].selecting = false;
            //}
   
            // Player presses A - advances character status to READY
            if (controller.Button_A && !joinedStatus[controller.playerId].selecting && joinedStatus[controller.playerId].isJoined && cards[controller.playerId].characterStatus != CharacterCardGenerator.CharacterStatus.READY && possibleCharacters[cards[controller.playerId].characterIndex].isTaken == false)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());
   
                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.READY;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[1];
                possibleCharacters[cards[controller.playerId].characterIndex].isTaken = true;

                ReadyCheck.instance.IncreasePlayersReady();
            }
   
            //Player presses B - reverts character status to previous state
            if (controller.sprint && !cards[controller.playerId].selecting && joinedStatus[controller.playerId].isJoined && cards[controller.playerId].characterStatus == CharacterCardGenerator.CharacterStatus.READY)
            {
                cards[controller.playerId].selecting = true;
                StartCoroutine(cards[controller.playerId].SelectionDelay());
   
                cards[controller.playerId].characterStatus = CharacterCardGenerator.CharacterStatus.SELECTING;
                cards[controller.playerId].readyStatusBar.sprite = cards[controller.playerId].statusSprites[0];
                possibleCharacters[cards[controller.playerId].characterIndex].isTaken = false;

                ReadyCheck.instance.DecreasePlayersReady();
            }
        }
    }
}
