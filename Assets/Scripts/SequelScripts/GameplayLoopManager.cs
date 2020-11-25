using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayLoopManager : MonoBehaviour
{

    public static GameplayLoopManager instance;
    public cameraShake shake;
    public delegate void NextTickEvent();
    public static event NextTickEvent onNextTickEvent;


    public static float TimeBetweenEvents { get; private set; }

    [Header("Event System")]
    [SerializeField] private float timeBetweenEvents;

    // TODO: Have pipes in scene add to a ship integrity value 
    [Header("Ship Statistics")]
    public int shipCurrenHealth;
    public int shipMaxHealth;

    [Header("Ship Blast Attributes")]
    [SerializeField] GameObject blastEffectPrefab;
    [SerializeField] float explosionRadius;
    public int explosionDamage;
    [SerializeField] LayerMask interactableLayerMask;
    [Space]
    [SerializeField] AttackLocation[] possibleAttackPositions;

    [HideInInspector] public Vector3 attackLocation;
    Vector3 lastHitLocaton;
    [HideInInspector] public bool gotHit;

    [Header("UI Elements")]
    public GameObject[] HealthBars;
    public GameObject loseGameScreen;
    int locationIndex;

    int index;
    private void Start()
    {
        TimeBetweenEvents = timeBetweenEvents;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < possibleAttackPositions.Length; i++)
        {
            //Debug.Log("I :" + i);
            for (int j = 0; j < Grid.instance.gridSizeX; j++)
            {
                //Debug.Log("J :" +j);
                for (int k = 0; k < Grid.instance.gridSizeY; k++)
                {
                    //Debug.Log("K :" +k);
                    if ((Vector3.Distance(Grid.instance.grid[j, k].worldPosition, possibleAttackPositions[i].worldPositon) <= explosionRadius))
                    {
                        if (Grid.instance.grid[j, k].isFlamable)
                        {

                            possibleAttackPositions[i].nodes.Add(Grid.instance.grid[j, k]);
                        }
                    }
                }
            }
        }

        shipCurrenHealth = shipMaxHealth;

        StartCoroutine("eventSystem");
        AdjustUI();
    }

    IEnumerator eventSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeBetweenEvents);
            onNextTickEvent();
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
        Collider[] damagedObjects = Physics.OverlapSphere(attackLocation, explosionRadius, interactableLayerMask);
        StartCoroutine(shake.Shake(0.15f, 0.2f));
        index = Random.Range(0, possibleAttackPositions[locationIndex].nodes.Count);
        Grid.instance.GenerateLaserFire(possibleAttackPositions[locationIndex].nodes[index]);

        shipCurrenHealth -= explosionDamage;

        foreach (Collider damagedObject in damagedObjects)
        {
            IDamageable<int> caughtObject = damagedObject.GetComponent<IDamageable<int>>();
            if (caughtObject != null) caughtObject.TakeDamage(1);
        }

        //AudioEventManager.instance.PlaySound("bang", .8f, Random.Range(.2f, 1f), 0);
        AdjustUI();

        if (shipCurrenHealth <= 0)
        {
            LoseGame();
        }

        yield return new WaitForSeconds(1.5f);

        Destroy(newBlast);

        yield return null;
    }

    void AdjustUI()
    {
        for (int i = 0; i < HealthBars.Length; i++)
        {
            if (shipCurrenHealth >= i)
            {
                HealthBars[i].SetActive(true);
            }
            else
            {
                HealthBars[i].SetActive(false);
            }


        }
    }

    void LoseGame()
    {
        // TODO: Make UI prettier and animate
        loseGameScreen.SetActive(true);
        GameStateManager.instance.SetGameState(GameState.LostByDamage);
        //Time.timeScale = Mathf.Lerp(1f, 0.2f, 2f);
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

                if (Gizmos.color == Color.red) { Gizmos.color = Color.red; } else { Gizmos.color = Color.blue; }
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(damagedObject.transform.position, new Vector3(0.8f, 0.8f, 0.8f));
                // MeshRenderer caughtObject = damagedObject.GetComponent<MeshRenderer>();
                //caughtObject.material.color = Color.red;
            }

        }
    }
}

