using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomManController : MonoBehaviour {

    NavMeshAgent agent;
    
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(new Vector3(transform.position.x + Random.Range(0, 1), 0 , transform.position.z + Random.Range(0, 1)));
	}
}
