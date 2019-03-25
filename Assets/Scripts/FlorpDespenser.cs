using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpDespenser : MonoBehaviour , IInteractable {

    public GameObject florpPrefab;
    public static FlorpDespenser instance;
    public Transform florpEjection;

 
    void Start ()
    {
		
	}

    public void InteractWith()
    {
        Instantiate(florpPrefab, florpEjection);
    }


}
