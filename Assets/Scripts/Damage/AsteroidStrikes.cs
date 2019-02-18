using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidStrikes : MonoBehaviour
{
    public GameObject[] alertSensors;
    public GameObject asteroid1;
    public Transform spawnPos;
    int randomItem;
    Vector3 strikePosDebug;
    Vector3 direction;

    [SerializeField] float strikeTime = 10f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(AsteroidStrike());
	}

    IEnumerator AsteroidStrike()
    {
        while (true)
        {
            float timeBetweenStrikes = Random.Range(3f, 10f);
            yield return new WaitForSeconds(timeBetweenStrikes);
            AlertRoom();
            yield return new WaitForSeconds(strikeTime);
            StrikeRoom();
        }
    }

    // Get rid of this, I'm just having the asteroids hit random spots on the ship
    void AlertRoom()
    {
        randomItem = Random.Range(0, 2);
        Debug.Log("Warning: " + alertSensors[randomItem] + "impact imminent!");
    }

    void StrikeRoom()
    {
        Vector3 strikePosition = new Vector3(Random.insideUnitSphere.x * 10f, Random.insideUnitSphere.y, Random.insideUnitSphere.z * 10f);
        direction = strikePosition - spawnPos.position;
        strikePosDebug = strikePosition;
        GameObject asteroid = Instantiate(asteroid1, spawnPos.position, Quaternion.identity);
        asteroid.GetComponent<Rigidbody>().AddForce(direction.normalized * 10f, ForceMode.Impulse);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, strikePosDebug.magnitude);
    //}
}
