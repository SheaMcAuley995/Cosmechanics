using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDoor : MonoBehaviour
{

    public Animator myAnimator;
    public Collider myCollider;
    public float doorTime;
    public bool isOpen = false;
    bool isRunning = false;

    private void Start() 
    {
        isOpen = !isOpen;
        StartCoroutine(doorTimer());
    }
    IEnumerator doorTimer()
    {
        while(true)
        {

          isOpen = !isOpen;

          myAnimator.SetBool("IsOpen", isOpen);
          myCollider.enabled = !isOpen;
          

          if (doorTime != 0)
          {
              isRunning = true;
              float time = doorTime;

              while (time > 0)
              {
                  time -= Time.deltaTime;
                  //Debug.Log(time);
                  yield return null;
              }

              isOpen = !isOpen;

              myAnimator.SetBool("IsOpen", isOpen);
              myCollider.enabled = !isOpen;
              
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
            yield return new WaitForSeconds(doorTime);
        }
    }
}
