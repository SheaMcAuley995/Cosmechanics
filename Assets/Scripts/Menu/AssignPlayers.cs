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
            newCharCard.GetComponent<CharacterCardGenerator>().GenerateCard();
            newCharCard.GetComponent<SelectionInput>().playerId = currentPlayerId;
            currentPlayerId++;
        }
        characterCards = GameObject.FindGameObjectsWithTag("CharacterCard");
    }
}
