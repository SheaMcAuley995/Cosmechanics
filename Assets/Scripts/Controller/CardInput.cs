using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CardInput : MonoBehaviour
{
    //Rewired ID
    public int playerId = 0;
    [HideInInspector] public Player player;

    [HideInInspector] public Vector2 selectModel;
    [HideInInspector] public bool selectCrime;
    [HideInInspector] public bool previousCrime;
    [HideInInspector] public bool selectColourRight;
    [HideInInspector] public bool selectColourLeft;
    [HideInInspector] public bool readyUp;
    [HideInInspector] public bool cancel;
    [HideInInspector] public bool start;


    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    public void GetInput()
    {
        selectModel.x = player.GetAxisRaw("ModelSelect");
        selectCrime = player.GetButtonDown("SelectCrime");
        previousCrime = player.GetButtonDown("PrevCrime");
        selectColourRight = player.GetButtonDown("ColourSelectRight");
        selectColourLeft = player.GetButtonDown("ColourSelectLeft");
        readyUp = player.GetButtonDown("ReadyUp");
        cancel = player.GetButtonDown("Cancel");
        start = player.GetButtonDown("Start");
    }
}
