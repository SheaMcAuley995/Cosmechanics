using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResetBox : MonoBehaviour {


    List<Transform> transforms;

	void Start ()
    {
       //Interactable[] drobables = FindObjectsOfType<Interactable>();
       
        //^^^ this was an idea initally for a more complex way of going about it that i may revisit if we have to
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == null)
        {
            other.transform.position = new Vector3(0, 1, 0);
        }
        else
        {
           Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            other.transform.position = new Vector3(0, 1, 0);
            rb.isKinematic = false;
        }
        
    }
    void Update ()
    {
		
	}
}
