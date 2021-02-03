using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : PickUp
{
    public ParticleSystem waterHoseEffect;
    public bool isExtinguishing = false;
    //public PlayerController playerController;
    [SerializeField] BoxCollider box;
    [SerializeField] GameObject tutorialUI;
    [SerializeField] bool isTutorial = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // box = GetComponentInChildren<BoxCollider>();
        box.enabled = false;
    }

    public override void myInteraction()
    {
        base.myInteraction();

        if (playerController != null)
        {
            AudioEventManager.instance.PlaySound("Snuffer Spray Loop 1");
            waterHoseEffect.Play();
            box.enabled = true;
        }

    }


    public override void endMyInteraction()
    {
        base.endMyInteraction();
        AudioEventManager.instance.StopSound("Snuffer Spray Loop 1");
        waterHoseEffect.Stop();
        box.enabled = false;

    }

    public override void pickMeUp(Transform pickUpTransform)
    {
        if(isTutorial)
        {
            tutorialUI.SetActive(false);
        }
        base.pickMeUp(pickUpTransform);
    }
}