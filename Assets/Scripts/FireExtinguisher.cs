using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public ParticleSystem waterHoseEffect;
    public bool isExtinguishing = false;
    public PlayerController playerController;
    BoxCollider box;

    private void Start()
    {
        box = GetComponent<BoxCollider>();    
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
    }
    //public void toolInteraction()
    //{
    //    StopCoroutine(UseExtinguisher());
    //    isExtinguishing = true;
    //    StartCoroutine(UseExtinguisher());
    //}
    //
    //IEnumerator UseExtinguisher()
    //{
    //    waterHoseEffect.Play();
    //    yield return new WaitForSeconds(2.5f);
    //    isExtinguishing = false;
    //    yield return null;
    //}
    //
    //public void StopExtinguisher()
    //{
    //    StopCoroutine(UseExtinguisher());
    //    waterHoseEffect.Stop();
    //    isExtinguishing = false;
    //}
}
