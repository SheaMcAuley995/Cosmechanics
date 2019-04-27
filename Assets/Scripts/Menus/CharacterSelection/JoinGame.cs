using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinGame : MonoBehaviour
{
    public CharacterCardGenerator card;
    [HideInInspector] public bool isJoined;

    public Image joinIcon;
    public TextMeshProUGUI joinText;

	// Use this for initialization
	void Start ()
    {
        isJoined = false;

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

    public void DisableJoinUI()
    {
        joinIcon.enabled = false;
        joinText.enabled = false;
    }
}
