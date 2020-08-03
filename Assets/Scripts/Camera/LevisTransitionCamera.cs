using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class LevisTransitionCamera : MonoBehaviour
{
    public Vector3 offset;

    public Quaternion baseRotation;

    List<GameObject> targets;
    public GameObject target;

    public float transitionSpeed;


    public List<GameObject> worlds;
    int world;

    public bool worldSelected;

    int currentTarget;

    private Vector2 moveAxis;

    public AudioSource menuAudio;

    Player player;

    private void Awake()
    {
        worldSelected = false;

        player = ReInput.players.GetPlayer(0);
    }

    private void Start()
    {
        player.AddInputEventDelegate(OnMoveHorizontal, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move Horizontal");
        player.AddInputEventDelegate(OnMoveHorizontal, UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed, "Move Horizontal");

        player.AddInputEventDelegate(OnBackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Back");

        player.AddInputEventDelegate(OnSelectPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Select");

        currentTarget = 0;

        targets = worlds;

        transform.rotation = baseRotation;

        if (targets != null)
        {
            transform.position = targets[0].transform.position + offset;
            target = targets[0];
        }
    }

    void OnMoveHorizontal(InputActionEventData data)
    {
        if (data.GetAxis() > 0)
        {
            NextTarget();
        }
        else if (data.GetAxis() < 0)
        {
            PrevTarget();
        }
    }

    void OnBackPressed(InputActionEventData data)
    {
        BackToWorldSelect();
    }

    void OnSelectPressed(InputActionEventData data)
    {      
        WorldSelect();
    }

    public void NextTarget()
    {
        currentTarget++;

        if (currentTarget >= targets.Count)
        {
            currentTarget = 0;
        }

        target = targets[currentTarget];
        StartCoroutine(ShiftCamera());
    }

    public void PrevTarget()
    {
        if (currentTarget == 0)
        {
            currentTarget = targets.Count - 1;
        }
        else
        {
            currentTarget--;
        }

        target = targets[currentTarget];
        StartCoroutine(ShiftCamera());
    }

    public void WorldSelect()
    {
        worldSelected = true;

        offset = new Vector3(0, 0, 5);

        world = currentTarget;
        currentTarget = 0;

        targets = target.GetComponent<LevelSecion>().levelList;

        SetOrbit(false);

        target = targets[0];


        StartCoroutine(ShiftCamera());
    }

    void SetOrbit(bool state)
    {
        foreach (GameObject level in targets)
        {
            OrbiterScript orbiter = level.GetComponent<OrbiterScript>();

            orbiter.doOrbit = state;

            if (state == true)
            {
                orbiter.moveBack = true;
            }
        }
    }

    public void BackToWorldSelect()
    {
        worldSelected = false;

        offset = new Vector3(0, 0, 25);

        SetOrbit(true);

        currentTarget = world;
        targets = worlds;
        target = worlds[currentTarget];


        StartCoroutine(ShiftCamera());
    }

    Vector3 velocity = Vector3.zero;

    IEnumerator ShiftCamera()
    {
        while (true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targets[currentTarget].transform.position + offset, ref velocity, transitionSpeed);

            yield return new WaitForFixedUpdate();
            //transform.parent = target.transform;
            transform.rotation = baseRotation;
        }
    }



}
