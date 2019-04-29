using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour 
{
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}
	#region Singleton

	public static ObjectPooling Instance;

	void Awake()
	{
		Instance = this;

	}

	#endregion
	public List<Pool>pools;

	public Dictionary<string , Queue<GameObject>> poolDoctionary;
	// Use this for initialization
	void Start () 
	{
		poolDoctionary = new Dictionary<string, Queue<GameObject>> ();
		foreach (Pool pool in pools)
		{
			Queue<GameObject> objPool = new Queue<GameObject> ();

			for (int i = 0; i < pool.size; i++) 
			{
				GameObject obj = Instantiate (pool.prefab);
				obj.SetActive (false);
				objPool.Enqueue (obj);
			}

			poolDoctionary.Add (pool.tag, objPool);
		}
	}


	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	{

		if (!poolDoctionary.ContainsKey (tag)) {
			Debug.Log("pool with tag " + tag +" doesnt exist");
			return null;
		}


		GameObject objectToSpawn =  poolDoctionary [tag].Dequeue ();

		objectToSpawn.SetActive (true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		IPooledObject poolObj =  objectToSpawn.GetComponent<IPooledObject> ();

		if (poolObj != null) {
			poolObj.OnObjectSpawn ();
		}

		poolDoctionary [tag].Enqueue (objectToSpawn);
		return objectToSpawn;
	}
}
