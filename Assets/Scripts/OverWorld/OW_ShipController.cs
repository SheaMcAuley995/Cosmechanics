using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_ShipController : MonoBehaviour {


    [Header("Movement")]
    public float speed;
    Vector2 shipDir;

    [Header("Gizmos")]
    public Color GizmoColor = Color.white;
    public Color SecondaryGizmoColor = Color.yellow;
    public Mesh gizmoMesh;
    public float gizmoMeshScale = 10f;
    Vector3 gScale;
    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        shipDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
	}


    private void OnDrawGizmos()
    {
        gScale = new Vector3(gizmoMeshScale, gizmoMeshScale, gizmoMeshScale);
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireMesh(gizmoMesh, 0, transform.position, transform.rotation, gScale);
        Gizmos.color = SecondaryGizmoColor;
        Gizmos.DrawLine(this.transform.position, shipDir.normalized);
    }
}
