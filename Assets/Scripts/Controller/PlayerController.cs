using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;


public class PlayerController : MonoBehaviour
{


    public delegate void currentInteraction();
    public currentInteraction myCurrentInteraction;

    public delegate void Interactions();
    public Interactions myInteractions;

    //Rewired ID
    public int playerId = 0;
    [HideInInspector] public Player player;

    [HideInInspector] public Vector2 movementVector;
    private Vector2 movementDir;
    [HideInInspector] public bool Interact;
    [HideInInspector] public bool sprint;
    [HideInInspector] public bool bumper;
    [HideInInspector] public bool pauseButton;

    [HideInInspector] public Vector2 selectModel;
    [HideInInspector] public bool selectCrime;
    [HideInInspector] public bool previousCrime;
    [HideInInspector] public bool selectColourRight;
    [HideInInspector] public bool selectColourLeft;
    [HideInInspector] public bool readyUp;
    [HideInInspector] public bool cancel;
    CharacterController cc;
    public bool normalMovement = true;

    [HideInInspector] public bool pickUp = false;
    public Transform pickUpTransform;

    // preReWired scripts
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpheight = 1;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    public Transform cameraTrans;

    Rigidbody rb;
    InteractWithInterface interact;
    public Animator[] animators;

    GameObject interactedObject;
    public float onFiretimer;
    public float onFireTimerCur;
    public GameObject onFireEffect;
    private bool onFire;

    private void Start()
    {
        onFireTimerCur = onFiretimer;
        animators = GetComponentsInChildren<Animator>();
        player = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        interact = GetComponentInChildren<InteractWithInterface>();
        interact.controller = this;
    }

    void Update()
    {
        getInput();
        ProcessInput();
        onFireCheck();
        onFireTimerCur = Mathf.Clamp(onFireTimerCur += Time.time, 0, onFiretimer);
    }

    public void getInput()
    {
        #region Main Game Input
        // Normal axis when player is not on fire
        if (normalMovement)
        {
            movementVector.x = player.GetAxisRaw("Move Horizontal"); // get input by name or action id
            movementVector.y = player.GetAxisRaw("Move Vertical");
        }
        // Flipped axis when a player is on fire
        else
        {
            movementVector.x = player.GetAxisRaw("Move Vertical"); // get input by name or action id
            movementVector.y = player.GetAxisRaw("Move Horizontal");
        }
        movementDir = movementVector.normalized;
        Interact = player.GetButtonDown("Interact");
        sprint = player.GetButton("Sprint");
        pickUp = player.GetButtonDown("PickUp");
        bumper = player.GetButtonDown("Bumper");
        pauseButton = player.GetButtonDown("Pause");
        #endregion

        #region Char Select Input
        selectModel.x = player.GetAxisRaw("ModelSelect");
        selectCrime = player.GetButtonDown("SelectCrime");
        previousCrime = player.GetButtonDown("PrevCrime");
        selectColourRight = player.GetButtonDown("ColourSelectRight");
        selectColourLeft = player.GetButtonDown("ColourSelectLeft");
        readyUp = player.GetButtonDown("ReadyUp");
        cancel = player.GetButtonDown("Cancel");
        #endregion
    }

    private void ProcessInput()
    {
        Move(movementVector, sprint);

        if (myCurrentInteraction != null)
        {
            myCurrentInteraction();
        }

        if (interact.interactableObject == null)
        {
            myCurrentInteraction -= pickUpInteraction;
        }

        if (Interact)
        {
            interact.InteractWithObject();
            Interaction();
        }

        if(pickUp)
        {
            interact.pickUpObject();
            animators[0].SetBool("ButtonPress", true);
            animators[1].SetBool("ButtonPress", true);
            animators[0].SetBool("ButtonPress", false);
            animators[1].SetBool("ButtonPress", false);
        }

    }
    public void pickUpInteraction()
    {
        interact.interactableObject.pickUpTransform = pickUpTransform;
    }
    public virtual void Interaction()
    {
        if (interact.interactableObject != null)
        {
            if (myInteractions == null)
            {
                if (myCurrentInteraction == null)
                {
                    myCurrentInteraction += pickUpInteraction;
                    interact.callInteract();
                }
                else
                {
                    interact.interactableObject.pickUpTransform = null;
                    interact.interactableObject = null;
                    myCurrentInteraction -= pickUpInteraction;
                    interact.callInteract();
                }
            }
            else if (myInteractions != null)
            {
                myInteractions();
                Debug.Log("Running " + myInteractions);
            }


        }
        else if (interact.interactableObject == null)
        {
            if (myCurrentInteraction != null)
            {
                myCurrentInteraction = null;
            }
        }
    }

    void Move(Vector2 inputDir, bool running)
    {
        if(!onFire)
        {
            animators[0].SetBool("OnFire", false);
            animators[1].SetBool("OnFire", false);
            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }

            float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            if (targetSpeed > 0)
            {
                animators[0].SetBool("Move", true);
                animators[1].SetBool("Move", true);
            }
            else
            {
                animators[0].SetBool("Move", false);
                animators[1].SetBool("Move", false);
            }

        }




        if (onFire)
        {
            animators[0].SetBool("OnFire", true);
            animators[1].SetBool("OnFire", true);
            onFireEffect.SetActive(true);

            if (inputDir != Vector2.zero)
            {
                float targetRotation = (Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y);
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }

            float targetSpeed = walkSpeed;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        }
        else
        {
            onFireEffect.SetActive(false);
        }



        rb.velocity = transform.forward * currentSpeed;

    }

    public void onFireCheck()
    {
        if (onFireTimerCur < onFiretimer / 2)
        {
            onFire = true;
        }
        else
        {
            onFire = false;
        }
    }

}
