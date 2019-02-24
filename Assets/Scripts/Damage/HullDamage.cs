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

    public GameObject[] pipes, walls;
    bool noCapacity, lowCapacity, mediumCapacity, highCapacity;
    [SerializeField] float lowShieldDamage, mediumShieldDamage, highShieldDamage;
    public float shipIntegrityDamage, pipeIntegrityDamage;
    LayerMask spaceDebris;
    Vector3 impactPos;
    int pipeIndex, wallIndex;

    public Texture damagedWallTex, repairedTexture;

    public float shieldEnergy;
    public float hullIntegrity;
    float shieldRepairTime = 5f;
    float hullRepairTime = 3f;
    float timer = 0f;
    bool repairingShields, repairingHull;

    public GameObject repairBarBG;
    public Image repairBar;


    void Start()
    {
        shieldEnergy = 100f;
        hullIntegrity = 100f;
        highCapacity = true;

        walls = GameObject.FindGameObjectsWithTag("Wall"); // Get rid of this after prototype
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
        if (other.gameObject.layer == 10)
        {
            #region Shield Capacity Check
            if (shieldEnergy > 66)
            {
                noCapacity = false;
                lowCapacity = false;
                mediumCapacity = false;
                highCapacity = true;
            }
            else if (shieldEnergy > 33 && shieldEnergy <= 66)
            {
                noCapacity = false;
                lowCapacity = false;
                mediumCapacity = true;
                highCapacity = false;
            }
            else if (shieldEnergy > 0 && shieldEnergy <= 33)
            {
                noCapacity = false;
                lowCapacity = true;
                mediumCapacity = false;
                highCapacity = false;
            }
            else if (shieldEnergy <= 0)
            {
                noCapacity = true;
                lowCapacity = false;
                mediumCapacity = false;
                highCapacity = false;
            }
            #endregion

            impactPos = other.transform.position;
            Destroy(other.gameObject);

            #region Shield Damage Check
            if (highCapacity)
            {
                TakeLowDamage();
            }
            else if (mediumCapacity)
            {
                TakeMediumDamage();
            }
            else if (lowCapacity)
            {
                TakeHighDamage();
            }
            else if (noCapacity)
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
    }
    #endregion

    /// MECHANIC:
    /// Hits on the ship cause shield AND integrity damage. Amount is based on shield capacity,
    /// which goes down with each hit. Dents cause low integrity damage, cracks medium, and 
    /// pipes high. Players can raise integrity by repairing damage, and raise shield capacity 
    /// by maintaining the shields. No shields = extreme integrity damage.
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
        int pipeIndex = Random.Range(0, pipes.Length);
        if (!pipes[pipeIndex].GetComponent<PipeMechanic>().isDamaged)
        {
            pipes[pipeIndex].GetComponent<PipeMechanic>().PipeBurst();
        }

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
        hullIntegrity -= shipIntegrityDamage;
        #endregion
    }

    void Die()
    {
        Debug.Log("Uh Oh!");
    }
    #endregion
}