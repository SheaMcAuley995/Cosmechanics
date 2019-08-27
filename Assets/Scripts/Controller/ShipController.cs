using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShipController : MonoBehaviour
{
    //Rewired ID
    public int playerId = 0;
    [HideInInspector] public Player player;

    [HideInInspector] public Vector2 movementVector;
    private Vector2 movementDir;
    [HideInInspector] public bool pickUp = false;
    [HideInInspector] public bool sprint;


    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    public void GetInput()
    {
        movementVector.x = player.GetAxisRaw("Move Horizontal"); // get input by name or action id
        movementVector.y = player.GetAxisRaw("Move Vertical");
        movementDir = movementVector.normalized;
        pickUp = player.GetButtonDown("PickUp");
        sprint = player.GetButton("Sprint");
    }
}
