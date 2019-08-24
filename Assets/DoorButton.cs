using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable {

    [SerializeField] GameObject[] doors;
    [SerializeField] float doorTime;
    bool isOpen = false;


    public void InteractWith()
    {
        isOpen = !isOpen;

        foreach(GameObject door in doors)
        {
            door.GetComponent<Animator>().SetBool("IsOpen", isOpen);
            door.GetComponent<Collider>().enabled = isOpen;
        }
    }

    IEnumerator doorTimer()
    {
        float time = doorTime;

        if(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        else
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Animator>().SetBool("IsOpen", isOpen);
                door.GetComponent<Collider>().enabled = isOpen;
            }
        }
    }

}
