using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {

    //Call this delegate to change the interaction depending on the item in the player's hands.
    public delegate void currentInteraction();
    public currentInteraction myCurrentInteraction;

    /// <summary>
    /// I need you to make a function that will change what is happening in this delegate. An example would be that if you pick up a broom then your current interaction
    /// is to sweep. So do all the functions you need to do in this delegate. I may have actually made two delegates for the same thing on accident I don't know.
    /// </summary>
    public delegate void Interactions();
    public Interactions myInteractions;

    //Rewired ID
    public int playerId = 0;
    private Player player;

    private Vector2 movementVector;
    private Vector2 movementDir;
    private bool PickUp;
    private bool sprint;
    private bool Interact;
    CharacterController cc;

    bool pickUp = false;
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
    Interact interact;


    private void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        interact = GetComponentInChildren<Interact>();
    }

    void Update () {
        getInput();
        ProcessInput();
    }

    private void getInput()
    {
        movementVector.x = player.GetAxisRaw("Move Horizontal"); // get input by name or action id
        movementVector.y = player.GetAxisRaw("Move Vertical");
        movementDir = movementVector.normalized;
        PickUp = player.GetButtonDown("PickUp");
        sprint = player.GetButton("Sprint");
        Interact = player.GetButtonDown("Interact");
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

        if (PickUp)
        {
            interact.InteractWith();
            Interaction();
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
            if(myInteractions == null)
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
            else if(myInteractions != null)
            {
                myInteractions();
                Debug.Log("Running " + myInteractions);
            }
            
            
        }
        else if (interact.interactableObject == null)
        {
            if(myCurrentInteraction != null)
            {
                myCurrentInteraction = null;
            }
        }
    }

    void Move(Vector2 inputDir, bool running)
    {

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);


        rb.velocity = transform.forward * currentSpeed;

    }


}
