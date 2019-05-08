using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptor : MonoBehaviour {


    [SerializeField] Engine engine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Florp>() != null)
        {
            if (other.GetComponent<Florp>().isFilled)
            {
                //EndGameScore.instance.AddInsertedFlorp(insertedFlorps);
                if (engine != null) { engine.InsertFlorp(); }

                BottleDispenser.instance.bottlesDispensed--;
                Destroy(other.gameObject);
                AudioEventManager.instance.PlaySound("reversesplat", .9f, 1, 0);
            }
            else { return; }
        }
    }
}
