using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject characterCardPrefab, panelParent;

	// Use this for initialization
	void Awake ()
    {
        for (int characterCards = 0; characterCards < ExampleGameController.instance.numberOfPlayers; characterCards++)
        {
            GameObject newCharCard = Instantiate(characterCardPrefab, panelParent.transform);
        }
    }
}
