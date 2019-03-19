using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ineedMaterial : MonoBehaviour {

    public PlayerInfo info;

    private void Start()
    {
        info = GetComponentInParent<PlayerInfo>();
        Debug.Log("My material index is " + GetComponentInParent<PlayerController>().playerId);
        Material myMat = GetComponent<MeshRenderer>().material = info.teamColor[GetComponentInParent<PlayerController>().playerId];


    }
}
	


