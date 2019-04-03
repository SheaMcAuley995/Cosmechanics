using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayers : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;
    int currentPlayerId = 0;

    void Awake()
    {
        CreateAndFindCards();
    }

    void CreateAndFindCards()
    {
        for (int totalCharCards = 0; totalCharCards < ExampleGameController.instance.numberOfPlayers; totalCharCards++)
        {
            GameObject newCharCard = Instantiate(characterCardPrefab, panelParent.transform);
            ExampleGameController.instance.setSpawnPoints();
            //newCharCard.GetComponent<PlayerController>().playerId = currentPlayerId;
            currentPlayerId++;
        }
        characterCards = GameObject.FindGameObjectsWithTag("CharacterCard");

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
    }
}
