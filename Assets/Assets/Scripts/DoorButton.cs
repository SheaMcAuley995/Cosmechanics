using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable {

    [SerializeField] GameObject[] doors;
    [SerializeField] float doorTime;
    bool isOpen = false;
    bool isRunning = false;

    public void InteractWith()
    {
        if(!isRunning)
        {
            StartCoroutine(doorTimer());
        }
    }

    IEnumerator doorTimer()
    {

        isOpen = !isOpen;

        foreach (GameObject door in doors)
        {
            door.GetComponent<Animator>().SetBool("IsOpen", isOpen);
            door.GetComponent<Collider>().enabled = !isOpen;
        }

        if (doorTime != 0)
        {
            isRunning = true;
            float time = doorTime;

            while (time > 0)
            {
                time -= Time.deltaTime;
                Debug.Log(time);
                yield return null;
            }

            isOpen = !isOpen;

            foreach (GameObject door in doors)
            {
                 door.GetComponent<Animator>().SetBool("IsOpen", isOpen);
                 door.GetComponent<Collider>().enabled = !isOpen;
            }
            isRunning = false;
        }
        else
        {
            float waitTime = 1.5f;
            isRunning = true;
            while(waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                yield return null;
            }
            isRunning = false;
        }
    }
}
