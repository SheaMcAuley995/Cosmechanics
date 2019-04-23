using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour {

    public GameObject laserPrefab;
    public Transform enemyShipPosition;
    Mesh mesh;
    private Vector3 impactPoint;
    public float laserSpeed= 100f;
    public List<GameObject> lasers;
    
    [Space]

    [Header("Gizmos")]
    public Color GizmoColor;
    [Range(1,5)]
    public float gizmoSize = 5;
    // Use this for initialization
    void Start () {
        Vector3 enemy = enemyShipPosition.position;

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 laserImpactPoint = ShipHealth.instance.attackLocation;
        impactPoint = laserImpactPoint; 
            if (ShipHealth.instance.gotHit)
            {    
                GameObject bolt = Instantiate(laserPrefab, enemyShipPosition.position, laserPrefab.transform.rotation);
                //laserPrefab.transform.LookAt(ShipHealth.instance.attackLocation);
                AudioEventManager.instance.PlaySound("enemyfire", .7f, 1, 1);
                bolt.transform.LookAt(laserImpactPoint);
                lasers.Add(bolt);
                ShipHealth.instance.gotHit = false;
            }
        ImAFirinMahLaser();
        
    }

    private void ImAFirinMahLaser()
    {
       
        foreach (var blast in lasers)
        {
            if (blast != null)
            {
                
                blast.transform.Translate(blast.transform.forward * laserSpeed * Time.deltaTime, Space.World);               
            }

        }      
    }
   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(ShipHealth.instance.attackLocation, gizmoSize);
        Gizmos.DrawSphere(enemyShipPosition.position, gizmoSize);
        Gizmos.DrawWireMesh(mesh, 0, enemyShipPosition.position, enemyShipPosition.rotation, new Vector3(1,1,1));
    }
}
