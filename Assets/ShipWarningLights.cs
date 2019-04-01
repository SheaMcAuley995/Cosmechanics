using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWarningLights : MonoBehaviour {

    public Material[] warningMats;
    Material myMat;
	// Use this for initialization
	void Start () {
        myMat = GetComponent<Renderer>().material;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (true/*ShipHealth health is < 66%*/)
        {
            warningMats[0] = myMat;
        }
        if (true/*ShipHealth health is < 33%*/)
        {
            warningMats[1] = myMat;
        }
        if (true/*ShipHealth health is < 10%*/)
        {
            warningMats[2] = myMat;
        }
        myMat = GetComponent<Renderer>().material;
    }
}
