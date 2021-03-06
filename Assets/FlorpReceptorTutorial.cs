﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpReceptorTutorial : MonoBehaviour {

    public int isFilled;
    public ParticleSystem particle;

    private void Start()
    {
        isFilled = 0;
    }

    public void fillTheEngine(Florp other)
    {

        particle.Play();
        //EndGameScore.instance.AddInsertedFlorp(insertedFlorps);
        //if (engine != null) { engine.InsertFlorp(); }

        BottleDispenser.instance.bottlesDispensed--;
        Destroy(other.gameObject);
        AudioEventManager.instance.PlaySound("reversesplat", .9f, 1, 0);
        isFilled++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Florp>() != null)
        {
            if (other.GetComponent<Florp>().isFilled)
            {
                particle.Play();
                //EndGameScore.instance.AddInsertedFlorp(insertedFlorps);
                isFilled++;

                BottleDispenser.instance.bottlesDispensed--;
                Destroy(other.gameObject);
                AudioEventManager.instance.PlaySound("reversesplat", .9f, 1, 0);
            }
            else { return; }
        }
    }
}
