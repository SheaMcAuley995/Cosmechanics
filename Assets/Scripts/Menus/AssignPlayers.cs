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
        //playButton = GameObject.FindGameObjectWithTag("PlayButton(CharSelect)").GetComponent<Button>();

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
        cards = FindObjectsOfType<CharacterCardGenerator>();

        #region This is really really bad I'm so sorry please don't judge me too hard
        // Assigns each player a spawn position
        switch (characterCards.Length)
        {
            case 1:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos1").transform.position;
                break;
            case 2:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos1").transform.position;
                characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos2").transform.position;
                break;
            case 3:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos1").transform.position;
                characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos2").transform.position;
                characterCards[2].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos3").transform.position;
                break;
            case 4:
                characterCards[0].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos1").transform.position;
                characterCards[1].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos2").transform.position;
                characterCards[2].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos3").transform.position;
                characterCards[3].GetComponent<CharacterCardGenerator>().spawnPos = GameObject.FindGameObjectWithTag("SpawnPos4").transform.position;
                break;
        }
        #endregion

        // Assigns each player a playerId and generates their first character
        playerControllers = FindObjectsOfType<PlayerController>();
        foreach (PlayerController playerController in playerControllers)
        {
            playerController.playerId = currentPlayerId;
            currentPlayerId++;
            cards[playerController.playerId].GenerateCard(playerController.playerId);
        }
        // Finds the new player controllers
        playerControllers = FindObjectsOfType<PlayerController>();
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

            // If a player presses a, generate a new character for that player based on their playerId
            if (controller.pickUp)
            {
                cards[controller.playerId].GenerateCard(controller.playerId);
            }

            // If a player presses x, press the play button (proceeds to level selection scene)
            if (controller.Interact)
            {
                // TODO: Make each player 'ready', and don't proceed until all players are ready
                playButton.onClick.Invoke();
            }
        }
    }
}
