using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerColor : MonoBehaviour {

    ineedMaterial info;
    Material mat;
    Color myColor;
	// Use this for initialization
	void Start () {
        info = GetComponentInParent<ineedMaterial>();
        
        
        myColor = GetComponent<Image>().color;
        myColor = info.color;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
