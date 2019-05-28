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
        //box = GetComponentInChildren<BoxCollider>();
        box.enabled = false;
    }

    public void Update()
    {
        if (playerController != null)
        {
            isExtinguishing = playerController.player.GetButton("Interact");

            if (isExtinguishing)
            {
                if (!waterHoseEffect.isPlaying)
                {
                    waterHoseEffect.Play();
                }
                box.enabled = true;
            }
            else
            {
                if (!waterHoseEffect.isStopped)
                {
                    waterHoseEffect.Stop();
                }
                box.enabled = false;
            }
        }
        else
        {
            waterHoseEffect.Stop();
        }
    }
}
