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
        //FUCK
    public bool isFilled =false;
    private int florpFilled = 1;

    public float florpFillAmount = 1.1f;

    //public ParticleSystem particle;

    private void Start()
    {
        doFill = false;
        isFilled = false;
        transform.localScale = zero;
        rb = GetComponent<Rigidbody>();
        initTime = Time.time;

    }
    public override void pickMeUp(Transform pickUpTransform)
    {
        base.pickMeUp(pickUpTransform);
        if(!isFilled) AudioEventManager.instance.PlaySound("bottledrop", .3f, Random.Range(.5f, .7f), 0);
        if(isFilled) AudioEventManager.instance.PlaySound("halfsplat", .3f, Random.Range(.5f, .7f), 0);
       
        
    }
    public void toolInteraction()
    {
        
        if (doFill)
        {
            
            Renderer rend = GetComponentInChildren<Renderer>();
            rend.material.SetFloat("_FillAmont", florpFillAmount);
            //particle.Play();
            isFilled = true;
            EndGameScore.instance.AddFlorpScore(florpFilled);
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
