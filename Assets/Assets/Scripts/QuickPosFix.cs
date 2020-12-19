using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickPosFix : MonoBehaviour {

    Vector3 correctPos = new Vector3(-5f, 0.05f, -13f);


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != correctPos)
        {
            transform.position = correctPos;
        }
	}
}
