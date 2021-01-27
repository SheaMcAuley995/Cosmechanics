using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using System;

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
    [HideInInspector] public bool Button_Y;
    [HideInInspector] public bool Button_X;
    [HideInInspector] public bool Button_RB;
    [HideInInspector] public bool Button_LB;
    [HideInInspector] public bool Button_A;
    [HideInInspector] public bool Button_B;
    [HideInInspector] public bool start;
    [HideInInspector] public bool blockMovement = false;
    CharacterController cc;
    public bool normalMovement = true;

    [HideInInspector] public bool pickUp = false;

    public Transform pickUpTransform;

    // preReWired scripts
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpheight = 1;
    [Range(0,1)]
    public float airControlPercent;
    [Range(0, 25)]
    public float dropSpeedPercent;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;
    public float groundcheckRaycastLength;
    public LayerMask floorLayer;

    public Transform cameraTrans;

    Rigidbody rb;
    public InteractWithInterface interact;
    public int maxPossibleCollisions;
    public LayerMask collisionLayer;
    public float radius;
    Collider[] possibleColliders;
    private Collider thisCollider;
    public BoxCollider holdPositionBoxCollider;
    public Animator animator;

    [HideInInspector] public GameObject interactedObject;
    public float onFiretimer;
    public float onFireTimerCur;
    public GameObject onFireEffect;
    private bool onFire;
    public Collider myCollider;
    public LayerMask interactableLayer;
    public Interactable interactableObject;
    bool pickedUp;
    float holdDownStartTime;

    public bool GetpickedUp() { return pickedUp; }
    public void SetpickedUp(bool val) { pickedUp = val; }

    public bool pause;
    private void Start()
    {
        holdPositionBoxCollider = GetComponent<BoxCollider>();
        holdPositionBoxCollider.enabled = false;
        thisCollider = GetComponent<CapsuleCollider>();
        possibleColliders = new Collider[maxPossibleCollisions];
        onFireTimerCur = onFiretimer;
        animator = GetComponent<Animator>();
        player = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        interact = GetComponentInChildren<InteractWithInterface>();
        interact.controller = this;

        pause = false;

        if (CharacterHandler.instance == null)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    void Update()
    {
        if (pause == false)
        {
            getInput();
            ProcessInput();
        }

        onFireCheck();
        onFireTimerCur = Mathf.Clamp(onFireTimerCur += Time.time, 0, onFiretimer);



        if(transform.position.y < -5)
        {
            transform.position = CharacterHandler.instance.spawnPoints[playerId];
            velocityY = 0;
        }
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
        //Interact = 
        sprint = player.GetButton("Sprint");
        pickUp = player.GetButtonDown("PickUp");
        
        bumper = player.GetButtonDown("Bumper");
        pauseButton = player.GetButtonDown("Pause");
        #endregion

        #region Char Select Input
        selectModel.x = player.GetAxisRaw("ModelSelect");
        Button_Y = player.GetButtonDown("SelectCrime");
        Button_X = player.GetButtonDown("PrevCrime");
        Button_RB = player.GetButtonDown("ColourSelectRight");
        Button_LB = player.GetButtonDown("ColourSelectLeft");
        Button_A = player.GetButtonDown("ReadyUp");
        Button_B = player.GetButtonDown("Cancel");
        start = player.GetButtonDown("Start");
        #endregion
    }

    private void ProcessInput()
    {
        float throwForce = 0;
        Move(movementVector, sprint);

        if (myCurrentInteraction != null)
        {
            myCurrentInteraction();
        }

        if (interact.interactableObject == null)
        {
            myCurrentInteraction -= pickUpInteraction;
        }



        if (player.GetButtonDown("Interact"))
        {
            //interact.InteractWithObject();
            Interaction();
        }

        if (player.GetButtonUp("Interact"))
        {
            endInteraction();
        }

        if(player.GetButtonDown("Pause"))
        {
            
        }

        if(pickedUp && player.GetButtonDown("PickUp") && interactedObject != null)
        {
             holdDownStartTime = Time.time;
             blockMovement = true;
        }
        else if(pickedUp && player.GetButtonUp("PickUp") && interactedObject != null)
        {
            //Debug.Log(throwForce);
            holdPositionBoxCollider.enabled = false;
            float holdDownTime = Time.time - holdDownStartTime;
            interactedObject.GetComponent<PickUp>().putMeDown(CalculateHoldDownForce(holdDownTime));
            interactedObject = null;
            animator.SetBool("isCarrying", false);
            blockMovement = false;
            pickedUp = false;
            return;
        }
        else if(player.GetButtonDown("PickUp") && interactedObject == null && !pickedUp)
        {
            pickUpObject();
        }
        else if(player.GetButtonUp("PickUp") && interactedObject != null && !pickedUp)
        {
            pickedUp = true;
        }

        



        if (player.GetButtonDown("Jump"))
        {
            Jump();
        }

    }


    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 1.5f;
        float HoldTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = HoldTimeNormalized * 50f;
        return force;
    }
    public void pickUpInteraction()
    {
        interact.interactableObject.pickUpTransform = pickUpTransform;
    }

    public virtual void Interaction()
    {

        if (interactedObject != null)
        {
            if (interactedObject.GetComponent<PickUp>() != null)
            {
                interactedObject.GetComponent<PickUp>().myInteraction();
            }
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                //Debug.Log("Interacting with :" + hitColliders[i].name);
                if (hitColliders[i].GetComponent<RepairableObject>() != null)
                {
                    if (hitColliders[i].GetComponent<RepairableObject>().health != hitColliders[i].GetComponent<RepairableObject>().healthMax)
                    {
                        if (animator != null) { animator.SetTrigger("Hammer"); }
                        animator.ResetTrigger("Hammer");
                        AudioEventManager.instance.PlaySound("Pipe repair");
                        hitColliders[i].GetComponent<IInteractable>().InteractWith();
                        return;
                    }
                }
                else
                {
                    if (hitColliders[i].GetComponent<IInteractable>() != null)
                    {
                        hitColliders[i].GetComponent<IInteractable>().InteractWith();
                    }
                    return;
                }

            }
        }
    }
    public void pickUpObject()
    {
        //Debug.Log("CAST");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward, radius, interactableLayer);
        // Debug.Log(transform.forward);
        if (interactedObject == null)
        {
            animator.SetBool("isCarrying", true);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<PickUp>() != null)
                {
                    AudioEventManager.instance.PlaySound("Pickup");
                    hitColliders[i].GetComponent<PickUp>().pickMeUp(pickUpTransform);
                    hitColliders[i].GetComponent<PickUp>().playerController = this;
                    //hitColliders[i].GetComponent<PickUp>().playerController = controller;
                    interactedObject = hitColliders[i].gameObject;
                    holdPositionBoxCollider.enabled = true;
                    if (hitColliders[i].GetComponent<Interactable>() != false)
                    {
                        interactableObject = hitColliders[i].GetComponent<Interactable>();
                    }
                    if (hitColliders[i].GetComponent<PickUp>().playerController != null)
                    {
                        break;
                    }
                }
            }
        }
        
    }

    public void endInteraction()
    {
        if (interactedObject != null)
        {
            if (interactedObject.GetComponent<PickUp>() != null)
            {
                //Debug.Log("TOOL INTEREACTION");
                interactedObject.GetComponent<PickUp>().endMyInteraction();
            }
        }
    }

    void Move(Vector2 inputDir, bool running)
    {
        if (cameraTrans == null) { cameraTrans = Camera.main.transform; }

        if (!onFire)
        {
            animator.SetBool("isOnFire", false);
            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetMotifiedSmoothTime(turnSmoothTime));
            }

            float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetMotifiedSmoothTime(speedSmoothTime));

            velocityY += Time.deltaTime * gravity;

           // if (targetSpeed > 0)
           // {
           //     animator.SetBool("Move", true);
           //
           // }
           // else
           // {
           //     animator.SetBool("Move", false);
           // }

        }

        if (onFire)
        {
            animator.SetBool("isOnFire", true);


            if (inputDir != Vector2.zero)
            {
                float targetRotation = (Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y);
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetMotifiedSmoothTime(turnSmoothTime));
            }

            float targetSpeed = walkSpeed;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetMotifiedSmoothTime(speedSmoothTime));
            velocityY += Time.deltaTime * gravity;
        }
        else
        {
            //onFireEffect.SetActive(false);
        }




        if (!blockMovement)
        {
            rb.velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        }
        else
        {
            rb.velocity = transform.forward * currentSpeed/2 + Vector3.up * velocityY;
        }

        float animationSpeedPercent = currentSpeed / walkSpeed;
        animator.SetFloat("Movement", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        if (Physics.Raycast(transform.position, Vector3.down, groundcheckRaycastLength, floorLayer))
        {
            velocityY = 0;
            //rb.velocity.y = 0;
        }
        else
        {
            if (dropSpeedPercent != 0)
            {
                velocityY -= Time.deltaTime * dropSpeedPercent;
            }
        }
    }
    private void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundcheckRaycastLength, floorLayer))
        {
            animator.SetTrigger("Jump");
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpheight);
            velocityY = jumpVelocity;
        }
    }

    float GetMotifiedSmoothTime(float smoothTime)
    {
        if(Physics.Raycast(transform.position, Vector3.down, groundcheckRaycastLength, floorLayer))
        {
            return smoothTime;
        }

        if(airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Physics.Raycast(transform.position, Vector3.down, groundcheckRaycastLength, floorLayer) ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y - groundcheckRaycastLength, transform.position.z));
    }
}
