using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMeshAnim : MonoBehaviour
{
    [SerializeField] float perlinScale = 1.0f;
    [SerializeField] float waveSpeed = 1.5f;
    [SerializeField] float waveHeight = 0.2f;

    Mesh mesh;
    Renderer rend;


    void Start()
    {
        if (!mesh) { mesh = GetComponent<MeshFilter>().mesh; }
        if (!rend) { rend = gameObject.GetComponent<Renderer>(); }
    }

    void Update()
    {
        AnimateMesh();
    }

    void AnimateMesh()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float pX = (vertices[i].x * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed);
            float pZ = (vertices[i].z * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed);
            vertices[i].y = (Mathf.PerlinNoise(pX, pZ) - 0.5f) * waveHeight;
        }
        mesh.vertices = vertices;

        Vector2 offset = new Vector2(Time.time * waveSpeed, 0.0f);
        rend.material.mainTextureOffset = offset;
    }
}
