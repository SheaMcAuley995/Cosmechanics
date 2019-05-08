using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventManager : MonoBehaviour{

    [SerializeField] public float explosionRadius;
    [SerializeField] public LayerMask interactableLayerMask;
    [SerializeField] public int explosionDamage;

    [SerializeField] GameObject[] Doors;
    List<Collider> doorCollider;
    List<Animator> doorAnimator;
    
    void Start () {
    
        Collider[] damagedObjects = Physics.OverlapSphere(transform.position, explosionRadius, interactableLayerMask);
    
        foreach (Collider damagedObject in damagedObjects)
        {
            IDamageable<int> caughtObject = damagedObject.GetComponent<IDamageable<int>>();
            //shipCurrenHealth -= explosionDamage;
            if (caughtObject != null) caughtObject.TakeDamage(explosionDamage);
            if (caughtObject != null) caughtObject.TakeDamage(explosionDamage);
        }

        for(int i = 0; i < Doors.Length; i++)
        {
            doorCollider[i] = Doors[i].GetComponent<Collider>();
            doorAnimator[i] = Doors[i].GetComponent<Animator>();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

        Collider[] damagedObjects = Physics.OverlapSphere(transform.position, explosionRadius, interactableLayerMask);

        foreach (Collider damagedObject in damagedObjects)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(damagedObject.transform.position, new Vector3(0.8f, 0.8f, 0.8f));
        }

    }
}
