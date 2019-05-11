using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : PickUp
{
    public ParticleSystem waterHoseEffect;
    public bool isExtinguishing = false;
    //public PlayerController playerController;
    BoxCollider box;

    private void Start()
    {
        box = GetComponentInChildren<BoxCollider>();
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
