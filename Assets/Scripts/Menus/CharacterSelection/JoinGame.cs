using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{
    public CharacterCardGenerator card;
    public bool isJoined;
    public bool selecting;

	// Use this for initialization
	void Start ()
    {
        isJoined = false;
        selecting = false;

        if (card == null)
        {
            card = GetComponent<CharacterCardGenerator>();
        }
	}

    public void CreateAndAssignPlayer(int id)
    {
        card.GenerateFullCard(id);
        isJoined = true;
    }

    public void UnjoinGame(int id)
    {
        card.RemovePlayer(id);
        isJoined = false;
    }

    public IEnumerator SelectionDelay()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }
}
