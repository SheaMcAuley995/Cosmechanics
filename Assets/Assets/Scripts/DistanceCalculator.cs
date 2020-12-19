using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour {

    [SerializeField] Transform target;

	void Update () {
        //Debug.Log(Vector3.Distance(transform.position, target.position));
       // Debug.DrawLine(transform.position, target.position);
	}
}
