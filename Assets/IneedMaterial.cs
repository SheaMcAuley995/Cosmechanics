using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IneedMaterial : MonoBehaviour {

    public PlayerInfo info;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = info.teamColor[GetComponentInParent<ExampleGameController>().myPLayerID];

    }
}
