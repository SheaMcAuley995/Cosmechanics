using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour, IInteractableTool
{
    public ParticleSystem waterHoseEffect;
    public bool isExtinguishing = false;

    public void toolInteraction()
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

    public void StopExtinguisher()
    {
        StopCoroutine(UseExtinguisher());
        waterHoseEffect.Stop();
        isExtinguishing = false;
    }
}
