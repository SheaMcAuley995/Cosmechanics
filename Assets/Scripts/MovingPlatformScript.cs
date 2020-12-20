using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    Vector3 currentTarget;
    Vector3 startPos;
    Vector3 prevPos;

    public List<Vector3> points;

    Vector3 currentVelocity;

    public float speed;
    public float pauseTime;
    public bool platformsRotate;

    float distance;

    float startTime;

    bool stop;

    int currentPoint;

    Rigidbody[] rigidbodies;

    public GameObject PlatLaserEnd;
    private void Start()
    {
        rigidbodies = new Rigidbody[5];
        startTime = Time.time;

        currentPoint = 0;


        startPos = points[currentPoint];
        prevPos = points[points.Count - 1];

        distance = Vector3.Distance(startPos, currentTarget);

        StartCoroutine(PlatformMovement());

        for (int i = 0; i < points.Count; i++)
        {
            Instantiate(PlatLaserEnd, points[i], Quaternion.identity); 
        }


    }


    void ChangeDirection()
    {
        startPos = transform.position;
        prevPos = points[currentPoint];

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


            for (int i = 0; i < points.Count; ++i)
            {
                if (transform.position == points[i] && points != null)
                {
                    if (platformsRotate)
                    {
                        StartCoroutine(RotatePlatform(i + 1));
                    }

                    if (i == 0)
                    {
                        prevPos = points[points.Count - 1];

                        if (prevPos == points[points.Count - 1])
                        {
                            yield return new WaitForSeconds(pauseTime);
                            ChangeDirection();
                        }
                    }
                    else
                    {
                        if (prevPos == points[i - 1])
                        {
                            yield return new WaitForSeconds(pauseTime);
                            ChangeDirection();
                        }
                    }
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

    IEnumerator RotatePlatform(int nextPointIndex)
    {
        Vector3 direction;

        if (nextPointIndex == points.Count)
        {
            direction = points[0] - transform.position;
        }
        else
        {
            direction = points[nextPointIndex] - transform.position;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        while (transform.rotation.y != targetRotation.y)
        {
            if (pauseTime > 0) // Rotate during the pause
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (pauseTime * 100.0f) * Time.deltaTime);
            }
            else // Rotate fast enough that it looks like it rotates while still over the marker
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500.0f * Time.deltaTime);
            }

            yield return new WaitForEndOfFrame();
        }

        yield return null;
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

    private void OnDrawGizmosSelected()
    {
        for(int i = 0; i < points.Count;i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i], .5f);

            Gizmos.color = Color.white;
            if(points[i+1] != null)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }

        }
        
    }
}
