using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomGuy : MonoBehaviour {

    public GameObject target;
    NavMeshAgent agent;
    public string word = "word";

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
       //transform.LookAt(target.transform);
        Vector3 dir = (target.transform.position - transform.position).normalized;
        agent.Move(dir);
	}

    private void ThrowItem()
    {
        
    }
}
