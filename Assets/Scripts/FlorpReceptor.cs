using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptor : MonoBehaviour {


    [SerializeField] Engine engine;
    public ParticleSystem particle;

    public int isFilledAmount;

    public void Start()
    {
        isFilledAmount = 0;
    }

    public void fillTheEngine(Florp other)
    {

        particle.Play();
        //EndGameScore.instance.AddInsertedFlorp(insertedFlorps);
        if (engine != null) { engine.InsertFlorp(); }

        BottleDispenser.instance.bottlesDispensed--;
        Destroy(other.gameObject);
        AudioEventManager.instance.PlaySound("reversesplat", .9f, 1, 0);
        isFilledAmount++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Florp>() != null)
        {
            if (other.GetComponent<Florp>().isFilled)
            {
                particle.Play();
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
