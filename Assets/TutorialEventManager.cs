using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventManager : MonoBehaviour{

    public delegate void currentTutorial();
    currentTutorial myTutorial;

    int Stages;
    [SerializeField] RepairableObject[] pipes;
    [SerializeField] Grid grid;
    

    [SerializeField] public float explosionRadius;
    [SerializeField] public LayerMask interactableLayerMask;
    [SerializeField] public int explosionDamage;

    [SerializeField] GameObject[] Doors;
    [SerializeField] List<Collider> doorCollider;
    [SerializeField] List<Animator> doorAnimator;




    
    void Start () {

        grid = Grid.instance;
        myTutorial = checkPipes;

        Collider[] damagedObjects = Physics.OverlapSphere(transform.position, explosionRadius, interactableLayerMask);
    
        foreach (Collider damagedObject in damagedObjects)
        {
            IDamageable<int> caughtObject = damagedObject.GetComponent<IDamageable<int>>();
            //shipCurrenHealth -= explosionDamage;
            if (caughtObject != null) caughtObject.TakeDamage(explosionDamage);
            if (caughtObject != null) caughtObject.TakeDamage(explosionDamage);
        }

        //for(int i = 0; i < Doors.Length; i++)
        //{
        //    doorCollider[i] = Doors[i].GetComponent<Collider>();
        //    doorAnimator[i] = Doors[i].GetComponent<Animator>();
        //}
    }

    private void Update()
    {
        myTutorial();

    }

    void checkPipes()
    {
        int pipeMax = 0;
        int pipeCur = 0;
        foreach (RepairableObject pipe in pipes)
        {
            pipeMax += pipe.healthMax;
            pipeCur += pipe.health;
        }

        if(pipeMax == pipeCur)
        {
            doorAnimator[0].SetBool("Open", true);
            myTutorial = checkFire;
            //Debug.Log("WORKS");
        }
    }

    void checkFire()
    {
        bool isAllChecked = true;
        for (int x = 0; x < grid.gridSizeX; x++)
        {
            for (int y = 0; y < grid.gridSizeX; y++)
            {
                if (isAllChecked)
                {
                    isAllChecked = grid.grid[x,y].isFlamable;
                }
            }
        }
        if(isAllChecked)
        {
            Debug.Log("WORKS 2");
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
