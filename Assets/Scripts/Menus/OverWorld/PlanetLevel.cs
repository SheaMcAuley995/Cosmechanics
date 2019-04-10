using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetLevel : MonoBehaviour {

    Material planetMat;
    public Material selectedPlanet;
    MeshRenderer planetRenderer;

	// Use this for initialization
	void Start () {
        planetMat = GetComponent<Renderer>().material;
        planetRenderer = GetComponent<MeshRenderer>();
	}
	
    private void OnTriggerEnter(Collider other)
    {
        planetRenderer.material = selectedPlanet;

    }
    private void OnTriggerExit(Collider other)
    {
        planetRenderer.material = planetMat;

    }
}
