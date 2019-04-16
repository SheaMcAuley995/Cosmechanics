using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipHealth : MonoBehaviour {

    public static ShipHealth instance;

    public delegate void DamageAction();
    public static event DamageAction onDamagedAction;

    [Header("Event System")]
    [SerializeField] float timeBetweenNEvents;

    // TODO: Have pipes in scene add to a ship integrity value 
    [Header("Ship Statistics")]
    public int shipCurrenHealth;
    public int shipMaxHealth;

    [Header("Ship Blast Attributes")]
    [SerializeField] GameObject blastEffectPrefab;
    [SerializeField] float explosionRadius;
    [SerializeField] int explosionDamage;
    [SerializeField] LayerMask interactableLayerMask;
    [Space]
    [SerializeField] AttackLocation[] possibleAttackPositions;
    int locationIndex;

    [HideInInspector]public Vector3 attackLocation;
    //public List<Node> nodes = new List<Node>();
    Vector3 lastHitLocaton;
    [HideInInspector] public bool gotHit;           //michael add

    [Header("UI Elements")]
    public Slider healthBar;
    public GameObject loseGameScreen;

    int index;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        for(int i = 0; i < possibleAttackPositions.Length; i++)
        {
            //Debug.Log("I :" + i);
            for(int j = 0; j < Grid.instance.gridSizeX; j++)
            {
                //Debug.Log("J :" +j);
                for (int k = 0; k < Grid.instance.gridSizeY; k++)
                {
                    //Debug.Log("K :" +k);
                    if ((Vector3.Distance(Grid.instance.grid[j,k].worldPosition , possibleAttackPositions[i].worldPositon) <= explosionRadius))
                    {
                        if(Grid.instance.grid[j, k].isFlamable)
                        {
                            
                            possibleAttackPositions[i].nodes.Add(Grid.instance.grid[j, k]);
                        }
                    }
                }
            }
        }




        //shipCurrenHealth = shipMaxHealth;
        StartCoroutine("eventSystem");
        AdjustUI();
    }

    IEnumerator eventSystem()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenNEvents);
            StartCoroutine("shipBlast");
        }
    }

    IEnumerator shipBlast()
    {
        if (attackLocation != null)
        {
            while (attackLocation == lastHitLocaton)
            {
                locationIndex = Random.Range(0, possibleAttackPositions.Length);
                attackLocation = possibleAttackPositions[locationIndex].worldPositon;
            }
        }
        else
        {
            attackLocation = possibleAttackPositions[Random.Range(0, possibleAttackPositions.Length)].worldPositon;
        }

        lastHitLocaton = attackLocation;
        gotHit = true;                          //michael add
        yield return new WaitForSeconds(.5f);     //delay in travel time of laser

        GameObject newBlast = Instantiate(blastEffectPrefab, attackLocation, Quaternion.identity);

        
        index = Random.Range(0, possibleAttackPositions[locationIndex].nodes.Count);
        Grid.instance.GenerateLaserFire(possibleAttackPositions[locationIndex].nodes[index]);

        Collider[] damagedObjects = Physics.OverlapSphere(attackLocation, explosionRadius, interactableLayerMask);

        foreach(Collider damagedObject in damagedObjects)
        {
            IDamageable<int> caughtObject = damagedObject.GetComponent<IDamageable<int>>();
            //shipCurrenHealth -= explosionDamage;
            if (caughtObject != null) caughtObject.TakeDamage(explosionDamage);
        }

        AudioEventManager.instance.PlaySound("bang",.8f,Random.Range(.2f,1f),0);
        AdjustUI();

        if (shipCurrenHealth <= shipMaxHealth * 0.25)
        {
           // LoseGame();
        }

        yield return new WaitForSeconds(1.5f);
       
        Destroy(newBlast);

        yield return null;
    }

    public void AdjustUI()
    {
        //Debug.Log(shipCurrenHealth / shipMaxHealth);
        healthBar.value = (float)shipCurrenHealth / shipMaxHealth;
        //healthBar.fillAmount =(float)shipCurrenHealth / shipMaxHealth;
       // healthText.text = shipCurrenHealth.ToString();
    }

    void LoseGame()
    {
        // TODO: Make UI prettier and animate
        loseGameScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (AttackLocation attackPosition in possibleAttackPositions)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPosition.worldPositon, explosionRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPosition.worldPositon, 0.5f);

            Collider[] damagedObjects = Physics.OverlapSphere(attackPosition.worldPositon, explosionRadius, interactableLayerMask);

            foreach (Collider damagedObject in damagedObjects)
            {

                //if (Gizmos.color == Color.red) { Gizmos.color = Color.red; } else { Gizmos.color = Color.blue; }
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(damagedObject.transform.position, new Vector3(0.8f, 0.8f, 0.8f));
               // MeshRenderer caughtObject = damagedObject.GetComponent<MeshRenderer>();
               // caughtObject.material.color = Color.red;
            }

        }
    }
}
