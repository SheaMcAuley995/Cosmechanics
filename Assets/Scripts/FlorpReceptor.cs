using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptor : MonoBehaviour {


    [SerializeField] Engine engine;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Florp>().isFilled)
        {
            engine.InsertFlorp();
            Destroy(other.gameObject);
            AudioEventManager.instance.PlaySound("reversesplat",.3f, Random.Range(.5f, .7f), 0);
        }
    }
}
