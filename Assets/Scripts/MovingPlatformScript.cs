using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    Vector3 currentTarget;
    Vector3 startPos;

    public List<Vector3> points;

    Vector3 currentVelocity;

    public float speed;
    public float pauseTime;

    float distance;

    float startTime;

    bool stop;

    int currentPoint;

    Rigidbody[] rigidbodies;

    private void Start()
    {
        rigidbodies = new Rigidbody[5];
        startTime = Time.time;

        currentPoint = 0;
        startPos = points[currentPoint];

        distance = Vector3.Distance(startPos, currentTarget);

        StartCoroutine(PlatformMovement());
    }


    void ChangeDirection()
    {
        startPos = transform.position;

        currentPoint++;

        if (currentPoint + 1 > points.Count)
        {
            currentPoint = 0;
        }

        currentTarget = points[currentPoint];

        startTime = Time.time;
        distance = Vector3.Distance(startPos, currentTarget);

    }

    IEnumerator PlatformMovement()
    {
        while (!stop)
        {
            float distCovered = (Time.time - startTime) * speed;

            float distanceFraction = (distCovered / distance);

            transform.position = Vector3.Lerp(startPos, currentTarget, distanceFraction);


            for(int i = 0; i < points.Count; ++i)
            {
                if(transform.position == points[i] && points != null)
                {
                    yield return new WaitForSeconds(pauseTime);
                    ChangeDirection();
                }
            }

            var dir = currentTarget - transform.position;


            for (int i = 0; i < rigidbodies.Length; i++)
            {
                if (rigidbodies[i] != null)
                {
                    rigidbodies[i].MovePosition(rigidbodies[i].position + dir.normalized * speed * Time.deltaTime);
                }
            }
                yield return new WaitForFixedUpdate();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        for(int i = 0; i < rigidbodies.Length; i++)
        {
            if(rigidbodies[i] == null)
            {
                rigidbodies[i] = collision.rigidbody;
                break;
            }
        }
        //collision.collider.transform.parent = transform;        
    }

    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            if (rigidbodies[i] == collision.rigidbody)
            {
                rigidbodies[i] = null;
                break;
            }
        }
        //collision.collider.transform.parent = null;
    }
}
