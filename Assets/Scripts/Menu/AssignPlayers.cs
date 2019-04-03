using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignPlayers : MonoBehaviour
{
    public Button playButton;
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    public PlayerController[] playerControllers;
    public CharacterCardGenerator[] cards;
    int currentPlayerId = 0;


    void Awake()
    {
        ExampleGameController.instance.setSpawnPoints();
        for (int i = 0; i < ExampleGameController.instance.numberOfPlayers; i++)
        {
            GameObject tempPlayer = ExampleGameController.instance.addPlayer();
            Destroy(tempPlayer, 2f);
        }

        playButton = GameObject.FindGameObjectWithTag("PlayButton(CharSelect)").GetComponent<Button>();

        CreateAndFindCards();
    }

    void CreateAndFindCards()
    {
        for (int totalCharCards = 0; totalCharCards < ExampleGameController.instance.numberOfPlayers; totalCharCards++)
        {
            GameObject newCharCard = Instantiate(characterCardPrefab, panelParent.transform);
            ExampleGameController.instance.setSpawnPoints();         
        }
        characterCards = GameObject.FindGameObjectsWithTag("CharacterCard");
        cards = FindObjectsOfType<CharacterCardGenerator>();
        #region .
        switch (characterCards.Length)
        {
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

        playerControllers = FindObjectsOfType<PlayerController>();
        foreach (PlayerController playerController in playerControllers)
        {
            playerController.playerId = currentPlayerId;
            currentPlayerId++;
            cards[playerController.playerId].GenerateCard(playerController.playerId);
        }
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

            if (controller.pickUp)
            {
                cards[controller.playerId].GenerateCard(controller.playerId);
            }

            if (controller.Interact)
            {
                playButton.onClick.Invoke();
            }
        }
    }
}
