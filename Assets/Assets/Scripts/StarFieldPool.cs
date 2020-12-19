using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFieldPool : MonoBehaviour {


    GameObject starPrefab;
    public int count;
    int lastSelected = 0;
    GameObject[] stars;
	// Use this for initialization
	void Start () {
        stars = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            var instance = Instantiate(starPrefab);
            instance.SetActive(false);
            stars[i] = instance;
        }
	}

    GameObject Instantiate(Vector3 pos, Quaternion rot)
    {
        for (int i = 0 ; i<stars.Length; i++)
        {
            if (!stars[i].activeSelf)
            {
                lastSelected = i;
                stars[i].SetActive(true);
                stars[i].transform.position = pos;
                stars[i].transform.rotation = rot;
                return stars[i];

            }
        }
        return null; 
    }
    void Destroy(GameObject gameObject)
    {

        gameObject.SetActive(false);
    }

}
