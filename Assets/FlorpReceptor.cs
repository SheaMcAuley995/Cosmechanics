using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptor : MonoBehaviour {


    [SerializeField] Engine engine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Florp")
        {
            engine.InsertFlorp();
            Destroy(other.gameObject);
        }
    }
}
