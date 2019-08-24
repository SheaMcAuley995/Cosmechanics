using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Florp : PickUp , IInteractableTool
{
    public bool isFilling = false;

    Vector3 zero = Vector3.zero;
    Vector3 one = Vector3.one;
    public float initTime;
    public float lerpTime = 1.25f;
    public AnimationCurve curve;
    public float containedFlorp = 50f;
    public bool doFill;
    public bool isFilled = false;
    private int florpFilled = 1;
    [Space]
    [Header("Inner")]
    public GameObject innerContObj;
    Renderer innerRenderer;

    public float florpFillAmount = 0.2f;

    //public ParticleSystem particle;

    public LayerMask interactableLayer;

    private void Start()
    {
        doFill = false;
        isFilled = false;
        transform.localScale = zero;
        rb = GetComponent<Rigidbody>();
        initTime = Time.time;
        innerRenderer = innerContObj.GetComponent<Renderer>();

    }
    public override void pickMeUp(Transform pickUpTransform)
    {
        base.pickMeUp(pickUpTransform);
        if (!isFilled) AudioEventManager.instance.PlaySound("bottledrop", .3f, Random.Range(.5f, .7f), 0);
        if (isFilled) AudioEventManager.instance.PlaySound("halfsplat", .3f, Random.Range(.5f, .7f), 0);


    }
    public void toolInteraction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1, interactableLayer);
        FlorpDespenser despenser = null;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            despenser = hitColliders[i].GetComponent<FlorpDespenser>();

            if(despenser != null)
            {

            }
        }

        if (doFill)
        {


            innerRenderer.material.SetFloat("_FillAmount", florpFillAmount);
           // Debug.Log("florp is filles");
            //particle.Play();
            isFilled = true;
            EndGameScore.instance.AddFlorpScore(florpFilled);
        }
     //   Debug.Log(name + " is being interacted with");
    }


    private void Update()
    {
        float timeSince = Time.time - initTime;

        float fracTime = timeSince / lerpTime;
        transform.localScale = Vector3.Lerp(zero, one, curve.Evaluate(fracTime));

        if (playerController != null)
        {
            isFilling = playerController.player.GetButton("Interact");

            if (isFilling)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f, interactableLayer);

                for(int i = 0; i < hitColliders.Length; i++)
                {
                    if(hitColliders[i].GetComponent<FlorpDespenser>() != null)
                    {
                        innerRenderer.material.SetFloat("_FillAmount", florpFillAmount);
                        // Debug.Log("florp is filles");
                        //particle.Play();
                        isFilled = true;
                        //EndGameScore.instance.AddFlorpScore(florpFilled);
                    }
                    else if(hitColliders[i].GetComponent<FlorpReceptor>() != null)
                    {
                        hitColliders[i].GetComponent<FlorpReceptor>().fillTheEngine(this);
                        playerController.myCollider.enabled = false;
                    }
                    else if(hitColliders[i].GetComponent<FlorpReceptorTutorial>() != null)
                    {
                        hitColliders[i].GetComponent<FlorpReceptorTutorial>().fillTheEngine(this);
                        playerController.myCollider.enabled = false;
                    }



                }

            }
        }
    }
}