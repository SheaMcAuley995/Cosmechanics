using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour, IInteractable
{
    public ParticleSystem waterHoseEffect;
    public bool isExtinguishing = false;

    public void InteractWith()
    {
        StopCoroutine(UseExtinguisher());
        isExtinguishing = true;
        StartCoroutine(UseExtinguisher());
    }

    IEnumerator UseExtinguisher()
    {
        waterHoseEffect.Play();
        yield return new WaitForSeconds(2.5f);
        isExtinguishing = false;
        yield return null;
    }
}
