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

    public GameObject[] pipes;
    public GameObject crackPrefab, dentPrefab, pipePrefab;
    bool noCapacity, lowCapacity, mediumCapacity, highCapacity;
    [SerializeField] float lowShieldDamage, mediumShieldDamage, highShieldDamage;
    public float dentIntegrityDamage, crackIntegrityDamage, pipeIntegrityDamage;
    LayerMask spaceDebris;
    Vector3 impactPos;

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
        /// Apply damage to pipes
        foreach (var pipe in pipes)
        {
            var damagedPipe = pipe.GetComponent<PipeMechanic>(); /// For ease of typing and less ugly code

            /// Removes "health" from pipes
            damagedPipe.damageThreshold -= 5f;

            /// If a pipe reaches 0 "health"
            if (damagedPipe.damageThreshold <= 0)
            {
                /// Bursts the pipe
                damagedPipe.PipeBurst();
            }
        }
        #endregion

        #region Mesh Damage
        /// Decides what type of damage to apply to the ship
        int damageType = Random.Range(1, 2);
        if (damageType == 1) /// Makes a dent in the hull (low integrity hit)
        {
            GameObject newDent = Instantiate(dentPrefab, impactPos, Quaternion.identity);
            hullIntegrity -= dentIntegrityDamage;
        }
        else if (damageType == 2) // Makes a crack in the hull (medium integrity hit)
        {
            GameObject newCrack = Instantiate(crackPrefab, impactPos, Quaternion.identity);
            hullIntegrity -= crackIntegrityDamage;
        }
        #endregion
    }

    void Die()
    {
        Debug.Log("Uh Oh!");
    }
    #endregion
}