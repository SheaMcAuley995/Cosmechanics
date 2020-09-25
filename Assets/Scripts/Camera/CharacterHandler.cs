using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct playerInformation
{
    public string playerID;
    public string playerName;
    public GameObject PlayerObject;

}

public class CharacterHandler : MonoBehaviour
{
    public static CharacterHandler instance = null;
    
    //public Material[] materials;
    public int numberOfPlayers;
    public CameraMultiTarget cameraMultiTarget;
    public GameObject playerPrefab;
    private int currentPlayerId = 0;
    public Vector3[] spawnPoints = null;
    public List<string> spawnableScenes;

    //debug
    [SerializeField]int sceneToLoad;
    public string sceneToLoadString;
    
    [SerializeField]List<playerInformation> playerInformation = new List<playerInformation>();
    [SerializeField] GameObject player;
    [SerializeField] SkinnedMeshRenderer renderer;
    [SerializeField] List<Material> playerMaterialList = new List<Material>(); 
   // [SerializeField] playerInformation player1;
   // [SerializeField] playerInformation player2;
   // [SerializeField] playerInformation player3;
   // [SerializeField] playerInformation player4;

    private void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //StartCoroutine(testSwitchMat());
    }


    public GameObject addPlayer(Vector3 spawnPosition)
    {
        GameObject target = GameObject.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        cameraMultiTarget = Camera.main.GetComponent<CameraMultiTarget>();

        target.GetComponent<PlayerController>().playerId = currentPlayerId++;
        target.GetComponent<PlayerController>().cameraTrans = cameraMultiTarget.GetComponent<Camera>().transform;

        return target;
    }

    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("Scene Loaded :" + sceneToLoad);
    }


    private IEnumerator testSwitchMat()
    {
        for(int i = 0; i < 4; i++)
        {
            Debug.Log("Testing i = " + i);
            renderer.material = playerMaterialList[i];

            yield return new WaitForSeconds(3);
        }
    }

    public void SetSpawnPoints()
    {
         if(spawnPoints == null)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                spawnPoints[i] = transform.position + new Vector3(i + 1, 0, 0);
            }
        }
    }


    //private void Start()
    //{
    //    InitializeGameStart();
    //}
    //
    //
    //private void InitializeGameStart()
    //{
    //        if (true)
    //        {
    //            setSpawnPoints();
    //            var targets = new List<GameObject>(numberOfPlayers);
    //
    //            for (int i = 0; i < numberOfPlayers; i++)
    //            {
    //                //targets.Add(addPlayer());
    //                cameraMultiTarget.SetTargets(targets.ToArray());
    //            }
    //
    //        }
    //
    //    for (int i = 1; i < 11; i++)
    //    {
    //        spawnableScenes[i-1] = "Ship_Level_" + i;
    //    }
    //
    //
    //   // SceneManager.activeSceneChanged += MakePlayers;
    //    SceneManager.activeSceneChanged += cameraCheck;
    //}
    //
    // private void cameraCheck(Scene current, Scene next)
    // {
    //     if (Camera.main.GetComponent<CameraMultiTarget>() != null)
    //     {
    //         cameraMultiTarget = Camera.main.GetComponent<CameraMultiTarget>();
    //     }
    //
    // }
    //
    //private void MakePlayers(Scene current, Scene next) {
    //
    //    string currentName = current.name;
    //
    //    if (currentName == null)
    //    {
    //        currentName = "Replaced";
    //    }
    //
    //    Debug.Log("Scenes: " + currentName + ", " + next.name);
    //
    //
    //    foreach(string scene in spawnableScenes)
    //    {
    //        if(currentName == scene)
    //        {
    //            var targets = new List<GameObject>(numberOfPlayers);
    //            Debug.Log(currentName + " works as a scene");
    //            for (int i = 0; i < numberOfPlayers; i++)
    //            {
    //                   
    //                targets.Add(addPlayer(spawnPoints[i]));
    //                cameraMultiTarget.SetTargets(targets.ToArray());
    //            }
    //        }
    //    }
    //}
    //

    //
    //
    //
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    for(int i = 0; i < numberOfPlayers; i++ )
    //    {
    //        Gizmos.DrawSphere(transform.position + new Vector3(i + 1, 0, 0), 0.5f);
    //    }
    //    
    //    
    //}

}