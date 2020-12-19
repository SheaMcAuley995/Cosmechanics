using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ineedMaterial : MonoBehaviour {

    public PlayerInfo info;
    public Color color;
    private void Start()
    {
        info = GetComponentInParent<PlayerInfo>();
        //Debug.Log("My material index is " + GetComponentInParent<PlayerController>().playerId);
        Material myMat = GetComponent<MeshRenderer>().material = info.teamMat[GetComponentInParent<PlayerController>().playerId];
        color = myMat.color;
    }
}
	


