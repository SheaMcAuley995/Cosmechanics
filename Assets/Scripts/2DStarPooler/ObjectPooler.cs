using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<Pool> pools;

	// Use this for initialization
	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
	}
	


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Vector3 direction, float shipSpeed)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exsist");
            return null;
        }


        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        if(objectToSpawn.GetComponent<StarMovement>() != null)
        {
            objectToSpawn.GetComponent<StarMovement>().startingPosition = transform.position;
            objectToSpawn.GetComponent<StarMovement>().shipSpeed = shipSpeed;
            objectToSpawn.GetComponent<StarMovement>().direction = direction;
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }


}
