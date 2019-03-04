using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SelectionInput : MonoBehaviour
{
    public int playerId = 0;
    Player players;

	// Use this for initialization
	void Awake ()
    {
        players = ReInput.players.GetPlayer(playerId);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
