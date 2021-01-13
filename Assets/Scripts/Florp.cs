using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Florp : PickUp
{
    //public bool doFill;
    public bool isTutorialFlorp;

    public float fillSpeed;

    public Renderer renderer;
    private MaterialPropertyBlock propertyBlock;

    public Material fullMat;
    public Material emptyMat;

    Renderer innerRenderer;

    //public float florpFillMax { 
    //    get; 
    //    private set; }

    public float florpFillMin = 0;
    public float florpFillMax = 4;



    public float florpFillAmount;

    public float amountFilled;
    //public AudioSource fillingAudio;
    //public ParticleSystem particle;

    public LayerMask FlorpFillerLayer;
    public FlorpFiller FlorpFiller;

    public LayerMask florpReceptorLayer;
    public FlorpReceptor florpReceptor;
    public FlorpReceptorTutorial FlorpReceptorTutorial;

    bool runFillLoop;
    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        //renderer.GetPropertyBlock(propertyBlock);
        florpFillAmount = florpFillMin;
        runFillLoop = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void fillFlorp()
    {
        if (florpFillAmount < florpFillMax)
        {
            //fillingAudio.Play();
            //propertyBlock.SetFloat("_FillAmouant", florpFillAmount);
            //renderer.SetPropertyBlock(propertyBlock);
            AudioEventManager.instance.PlaySound("Florp Container Fill");
            florpFillAmount += 1;
            renderer.material = fullMat;
        }
    }


    public override void myInteraction()
    {
        base.myInteraction();

        float timer = Time.time;

        if (florpFillAmount <= florpFillMax)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.TransformPoint(Vector3.zero), 2, florpReceptorLayer);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                florpReceptor = hitColliders[i].GetComponent<FlorpReceptor>();
            }
        }

        if (florpReceptor != null)
        {
            runFillLoop = true;
            StartCoroutine(fillingFlorp());
        }
    }

    public override void endMyInteraction()
    {
        runFillLoop = false;
        StopCoroutine(fillingFlorp());
        base.endMyInteraction();
    }

    public override void putMeDown(float throwForce)
    {
        endMyInteraction();
        base.putMeDown(throwForce);
        if (florpFillAmount < florpFillMax)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.TransformPoint(Vector3.zero), 2, FlorpFillerLayer);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                FlorpFiller = hitColliders[i].GetComponent<FlorpFiller>();
                if (FlorpFiller != null)
                {
                    FlorpFiller.curButton.On = true;
                    FlorpFiller.curButton.meshRenderer.material = FlorpFiller.buttonOnMat;
                    if (hitColliders[i] != null)
                    {
                        FlorpFiller.florp = this;
                        rb.isKinematic = true;
                        transform.position = FlorpFiller.holdPostion.position;
                        transform.rotation = FlorpFiller.holdPostion.rotation;
                        break;
                    }
                }
            }
        }
        else
        {
            //Collider[] hitColliders = Physics.OverlapSphere(transform.TransformPoint(Vector3.zero), 2, FlorpFillerLayer);
            //
            //for (int i = 0; i < hitColliders.Length; i++)
            //{
            //    FlorpReceptor receptor = hitColliders[i].GetComponent<FlorpReceptor>();
            //    
            //
            //}
        }
    }

    public override void pickMeUp(Transform pickUpTransform)
    {

        if (FlorpFiller == null)
        {
            base.pickMeUp(pickUpTransform);
        }


        if (FlorpFiller != null)
        {
            FlorpFiller.curButton.meshRenderer.material = FlorpFiller.buttonOffMat;
            FlorpFiller.curButton.On = false;
            FlorpFiller.florp = null;
            FlorpFiller = null;
            base.pickMeUp(pickUpTransform);
        }

    }

    IEnumerator fillingFlorp()
    {
        while (runFillLoop && (florpFillAmount > florpFillMin) && (florpReceptor.florpTotal < florpReceptor.florpMax))
        {
            florpReceptor.fillFlorp(1);

            if(florpFillAmount-- <= 0)
            {
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }

        }
        if (florpReceptor.CR_Running == false)
        {
            florpReceptor.CR_Running = true;
            florpReceptor.StartCoroutine(florpReceptor.burnFlorp());
        }

        if(florpFillAmount <= 0)
        {
            renderer.material = emptyMat;
        }

        endMyInteraction();
    }
}




