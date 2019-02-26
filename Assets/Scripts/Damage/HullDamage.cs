using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HullDamage : MonoBehaviour
{
    #region singleton
    public static HullDamage instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    [Header("Damageable Item Arrays")]
    public GameObject[] pipes;
    public GameObject[] walls;

    [Header("Settings")]
    public float wallIntegrityDamage;
    public float pipeIntegrityDamage;
    [SerializeField] float lowShieldDamage;
    [SerializeField] float mediumShieldDamage;
    [SerializeField] float highShieldDamage;

    public LayerMask spaceDebris;

    [Header("Art & UI")]
    public Texture damagedWallTex;
    public Texture repairedTexture;
    public GameObject repairBarBG;
    public Image repairBar;

    bool noShieldCapacity, lowShieldCapacity, mediumShieldCapacity, highShieldCapacity;
    Vector3 impactPos;
    int pipeIndex, wallIndex;

    [HideInInspector] public float shieldEnergy;
    [HideInInspector] public float hullIntegrity;
    float shieldRepairTime = 5f;
    float hullRepairTime = 3f;
    float timer = 0f;
    bool repairingShields, repairingHull;


    void Start()
    {
        shieldEnergy = 100f;
        hullIntegrity = 100f;
        highShieldCapacity = true;

        walls = GameObject.FindGameObjectsWithTag("Wall"); // Only using this because we haven't decided on a permanent level. Once we do, I'll hand-fill the array in the inspector
    }

    void Update()
    {
        if (repairingShields)
        {
            timer += 1f * Time.deltaTime;
            repairBar.fillAmount = timer / shieldRepairTime;
        }

        if (repairingHull)
        {
            timer += 1f * Time.deltaTime;
            repairBar.fillAmount = timer / hullRepairTime;
        }

        // TEMPORARY - FOR TESTING DAMAGE TO PIPES
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TakeUnshieldedDamage();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == spaceDebris)
        {
            #region Shield Capacity Check
            if (shieldEnergy > 66)
            {
                noShieldCapacity = false;
                lowShieldCapacity = false;
                mediumShieldCapacity = false;
                highShieldCapacity = true;
            }
            else if (shieldEnergy > 33 && shieldEnergy <= 66)
            {
                noShieldCapacity = false;
                lowShieldCapacity = false;
                mediumShieldCapacity = true;
                highShieldCapacity = false;
            }
            else if (shieldEnergy > 0 && shieldEnergy <= 33)
            {
                noShieldCapacity = false;
                lowShieldCapacity = true;
                mediumShieldCapacity = false;
                highShieldCapacity = false;
            }
            else if (shieldEnergy <= 0)
            {
                noShieldCapacity = true;
                lowShieldCapacity = false;
                mediumShieldCapacity = false;
                highShieldCapacity = false;
            }
            #endregion

            impactPos = other.transform.position;
            Destroy(other.gameObject);

            #region Shield Damage Check
            if (highShieldCapacity)
            {
                TakeLowDamage();
            }
            else if (mediumShieldCapacity)
            {
                TakeMediumDamage();
            }
            else if (lowShieldCapacity)
            {
                TakeHighDamage();
            }
            else if (noShieldCapacity)
            {
                TakeUnshieldedDamage();
            }
            #endregion

            if (hullIntegrity <= 0)
            {
                Die();
            }
        }
    }

    #region Repair Functions
    public IEnumerator RepairShipIntegrity(float amount)
    {
        repairBarBG.SetActive(true);
        repairingHull = true;

        yield return new WaitForSeconds(hullRepairTime);

        repairBarBG.SetActive(false);
        repairingHull = false;
        timer = 0f;

        hullIntegrity += amount;
        hullIntegrity = Mathf.Clamp(hullIntegrity, 0f, 100f);

        StopCoroutine(RepairShipIntegrity(amount));
    }

    public IEnumerator RepairShieldCapacity(float amount)
    {
        repairBarBG.SetActive(true);
        repairingShields = true;

        yield return new WaitForSeconds(shieldRepairTime);

        repairBarBG.SetActive(false);
        repairingShields = false;
        timer = 0f;

        shieldEnergy += amount;
        shieldEnergy = Mathf.Clamp(shieldEnergy, 0f, 100f);

        StopCoroutine(RepairShieldCapacity(amount));
    } // We don't have shields yet
    #endregion

    /// MECHANIC:
    /// Hits on the ship cause shield damage. Amount is based on shield capacity,
    /// which goes down with each hit. Once shields are destroyed, hits will cause 
    /// hull integrity damage. Players can raise integrity by repairing damage, and 
    /// raise shield capacity by maintaining the shields.
    #region Damage Functions
    void TakeLowDamage()
    {       
        shieldEnergy -= lowShieldDamage;
    }

    void TakeMediumDamage()
    {        
        shieldEnergy -= mediumShieldDamage;
    }
    
    void TakeHighDamage()
    {       
        shieldEnergy -= highShieldDamage;
    }

    void TakeUnshieldedDamage()
    {
        #region PipesDamage
        // CHEAT FOR PROTOTYPE ONLY!! DELETE & UNCOMMENT BELOW CODE AFTERWARDS YA DINGUS
      // int pipeIndex = Random.Range(0, pipes.Length);
      // if (!pipes[pipeIndex].GetComponent<PipeMechanic>().isDamaged)
      // {
      //     pipes[pipeIndex].GetComponent<PipeMechanic>().PipeBurst();
      // }

        /// Apply damage to pipes
        //foreach (var pipe in pipes)
        //{
        //    var damagedPipe = pipe.GetComponent<PipeMechanic>(); /// For ease of typing and less ugly code

        //    /// Removes "health" from pipes
        //    damagedPipe.damageThreshold -= 5f;

        //    /// If a pipe reaches 0 "health"
        //    if (damagedPipe.damageThreshold <= 0)
        //    {
        //        /// Bursts the pipe
        //        damagedPipe.PipeBurst();
        //    }
        //}
        #endregion

        #region Mesh Damage
        wallIndex = Random.Range(0, walls.Length); /// UPDATE NUMBER ONCE WE HAVE COUNTABLE WALLS
        walls[wallIndex].gameObject.GetComponent<Renderer>().material.mainTexture = damagedWallTex;
        walls[wallIndex].GetComponent<RepairableItem>().wallIsDamaged = true;
        hullIntegrity -= wallIntegrityDamage;
        #endregion
    }

    void Die()
    {
        Debug.Log("Uh Oh!");
    }
    #endregion
}