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
    public Material empty;
    public Material full;
    bool isFilled =false;


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
            myMat.Lerp(empty, full, 1);
            isFilled = true;   
        }
        Debug.Log(name + " is being interacted with");
    }


    private void Update()
    {
        float timeSince = Time.time - initTime;

        float fracTime = timeSince / lerpTime;
        transform.localScale = Vector3.Lerp(zero, one, curve.Evaluate(fracTime));
    }

}
