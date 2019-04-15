using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Florp : PickUp,IInteractableTool
{
    Vector3 zero = Vector3.zero;
    Vector3 one = Vector3.one;
    public float initTime;
    public float lerpTime = 1.25f;
    public AnimationCurve curve;
    public float containedFlorp = 50f;
    /*[HideInInspector]*/public bool doFill;

    public Material outerEmpty;
    public Material outerFull;
    public Material innerEmpty;
    public Material innerFull;
    public bool isFilled =false;
    public ParticleSystem particle;

    private void Start()
    {
        doFill = false;
        transform.localScale = zero;
        rb = GetComponent<Rigidbody>();
        initTime = Time.time;

    }
    public override void pickMeUp(Transform pickUpTransform)
    {
        AudioEventManager.instance.PlaySound("halfsplat", .3f, Random.Range(.5f, .7f), 0);
        base.pickMeUp(pickUpTransform);
    }
    public void toolInteraction()
    {
        
        if (doFill)
        {
            Material myMat = GetComponent<MeshRenderer>().material;
            Material myChildMat = GetComponentInChildren<MeshRenderer>().material;

            myChildMat.Lerp(innerEmpty, innerFull, 1);
            myMat.Lerp(outerEmpty, outerFull, 1);
            particle.Play();
            isFilled = true;   
        }
        Debug.Log(name + " is being interacted with");
    }


    private void Update()
    {
        float timeSince = Time.time - initTime;

        float fracTime = timeSince / lerpTime;
        transform.localScale = Vector3.Lerp((one * .6f), one, curve.Evaluate(fracTime));
    }

}
