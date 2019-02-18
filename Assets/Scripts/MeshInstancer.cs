
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MeshData
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;
    public float speed;
    internal Vector3 angularVelocity;
    public float seed;

    public Matrix4x4 matrix { get { return Matrix4x4.TRS(pos, rot, scale); } }
    

    public MeshData(Vector3 position,  Quaternion rotation, Vector3 scale, float speed)
    {
        this.pos = position;
        this.scale = scale;
        this.rot = rotation;
        this.speed = speed;
        this.seed = Random.value;
        this.angularVelocity = Random.insideUnitSphere;
    }
}
public class MeshInstancer : MonoBehaviour {


    public Mesh mesh;
    public Material mat;

    [Range(1, 1023)]
    public int maxAsteroids = 300;
    public float radius = 200f;
    private List<MeshData> asteroids = new List<MeshData>();
    public Transform spawnpoint;
    public float spawnMax = 10f;
    public float spawnMin = -10f;
    public float scaleValue = .5f;
    [Range(-25,25)]
    public float speedMult = 1f;
    float randomness;
    public Transform target;
    // Use this for initialization
    void Start () {
        randomness = Random.Range(spawnMin, spawnMax);
        if (asteroids.Count < maxAsteroids)
        {
            CreateAsteroid();
        }
	}

    private void CreateAsteroid()
    {
        float random2 = Random.Range(spawnMin, spawnMax);
        float random3 = Random.Range(spawnMin, spawnMax);
        var center = (this.transform.position + new Vector3(randomness, random2, random3));
        asteroids.Add(new MeshData(center, this.transform.rotation, this.transform.localScale, Random.Range(2,10)));
    }

    
    void Update ()
    {
        Vector3 asteroidHeading =Vector3.right;
        asteroidHeading.Normalize();
        foreach (var asteroid in asteroids)
        {
            var diff = asteroid.pos - this.transform.position;

            asteroid.pos += asteroidHeading * asteroid.speed *  Time.deltaTime * speedMult;
            asteroid.scale = new Vector3(scaleValue,scaleValue,scaleValue);
            if ((asteroid.pos - target.position).magnitude > radius)
            {
                ReplaceAsteroid(asteroid, asteroidHeading);
            }

            var angle = Mathf.Atan2(diff.x, diff.y);
            asteroid.rot = Quaternion.Euler(asteroid.angularVelocity * Time.time * 5 * asteroid.speed);
        }
        Graphics.DrawMeshInstanced(mesh, 0, mat, asteroids.Select((a)=>a.matrix).ToList());
	}

    private void ReplaceAsteroid(MeshData asteroid, Vector3 asteroidHeading)
    {
        randomness = Random.Range(spawnMin, spawnMax);
        float random2 = Random.Range(spawnMin, spawnMax);
        float random3 = Random.Range(spawnMin, spawnMax);
        asteroid.pos = (this.transform.position + new Vector3(randomness,random2,random3));
    }
}
