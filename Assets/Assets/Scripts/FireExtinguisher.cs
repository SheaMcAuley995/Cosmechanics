using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : PickUp
{
    public ParticleSystem waterHoseEffect;
    public bool isExtinguishing = false;
    //public PlayerController playerController;
    [SerializeField] BoxCollider box;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponentInChildren<BoxCollider>();
        box.enabled = false;
    }

    public override void myInteraction()
    {
        base.myInteraction();

        if (playerController != null)
        {
            waterHoseEffect.Play();
            box.enabled = true;
        }

    }


    public override void endMyInteraction()
    {
        base.endMyInteraction();

        waterHoseEffect.Stop();
        box.enabled = false;

    }
}