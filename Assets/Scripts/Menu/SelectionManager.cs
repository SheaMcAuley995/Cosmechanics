using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;
    public GameObject[] characterCards;

	// Use this for initialization
	void Awake ()
    {
        for (int characterCards = 0; characterCards < ExampleGameController.instance.numberOfPlayers; characterCards++)
        {
            GameObject newCharCard = Instantiate(characterCardPrefab, panelParent.transform);
        }

        characterCards = GameObject.FindGameObjectsWithTag("CharacterCard");
    }

    /// yeah this is gonna need some seeeerious re-working after prototype, fam
    void Update()
    {
        #region Don't Look At Me I'm Hideous
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            characterCards[0].GetComponent<CharacterCardGenerator>().GenerateCard();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            characterCards[1].GetComponent<CharacterCardGenerator>().GenerateCard();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            characterCards[2].GetComponent<CharacterCardGenerator>().GenerateCard();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterCards[3].GetComponent<CharacterCardGenerator>().GenerateCard();
        }
        #endregion
    }
}
