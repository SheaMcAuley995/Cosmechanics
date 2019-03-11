using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_ShipController : MonoBehaviour {


    [Header("Movement")]
    public float speed = 10f;
    
    public float rotationSpeed = 5f;
    private float horiz;
    private float vert;
    Vector2 shipDir;
    Rigidbody rb;

    [Header("Gizmos")]
    public Color GizmoColor = Color.white;
    public Color SecondaryGizmoColor = Color.yellow;
    public Mesh gizmoMesh;
    public float gizmoMeshScale = 10f;
    Vector3 gScale;

    

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        horiz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        shipDir = new Vector2(horiz, vert);
        this.transform.position += vert * transform.forward * speed * Time.deltaTime;
        var rotatoChip = rotationSpeed * Time.deltaTime * horiz;
        this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotatoChip, 0);
        Debug.Log("rotation speed = " + rotatoChip);
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
