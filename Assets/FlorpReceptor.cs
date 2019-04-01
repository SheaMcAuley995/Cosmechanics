using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptor : MonoBehaviour, IInteractable {

    public float maxFlorpAmount;
    public float currentFlorp;

    private void Start()
    {
        maxFlorpAmount = 200f;
    }
    public void InteractWith()
    {
        if (true /*holding florp?*/)
        {
            
            //remove florp from player
            //add amount florp to engines
        }
        Debug.Log("current florp = " + currentFlorp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Florp")
        {
            currentFlorp += other.GetComponent<Florp>().containedFlorp;
            Destroy(other.gameObject);
        }
    }
}
