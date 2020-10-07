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
    //[SerializeField]int sceneToLoad;
    public string sceneToLoadString;

    [SerializeField] List<playerInformation> playerInformation = new List<playerInformation>();
    [SerializeField] GameObject player;
    [SerializeField] SkinnedMeshRenderer renderer;
    [SerializeField] List<Material> playerMaterialList = new List<Material>();
    [SerializeField] List<PlayerController> playerControllers = new List<PlayerController>();
    public GameObject[] players = new GameObject[4];
    public Vector3[] spawnpoints;
    // [SerializeField] playerInformation player1;
    // [SerializeField] playerInformation player2;
    // [SerializeField] playerInformation player3;
    // [SerializeField] playerInformation player4;

    private void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log(gameObject + "is being destroyed");
            Destroy(gameObject);
        }

        //StartCoroutine(testSwitchMat());
    }

    private void Start()
    {
        players = new GameObject[CharacterHandler.instance.numberOfPlayers];
    }


    public GameObject addPlayer(Vector3 spawnPosition)
    {
        GameObject target = GameObject.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        cameraMultiTarget = Camera.main.GetComponent<CameraMultiTarget>();

        target.GetComponent<PlayerController>().playerId = currentPlayerId++;
        target.GetComponent<PlayerController>().cameraTrans = cameraMultiTarget.GetComponent<Camera>().transform;

        playerControllers.Add(target.GetComponent<PlayerController>());

        return target;
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
}
//public IEnumerator Transition(string name)
//{
//    //DontDestroyOnLoad(gameObject);
//    Debug.Log("Scene Loading :" + name);
//    yield return SceneManager.LoadSceneAsync(name);
//    Debug.Log("Scene Loaded :" + name);
//
//    if (name.Contains("Ship_Level_1"))
//    {
//        int j = 0;
//        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
//        {
//            players[j] = player.gameObject;
//            j++;
//        }
//
//        spawnpoints = FindObjectOfType<SetSpawnPositions>().spawnpositions;
//        CameraMultiTarget.instance.SetTargets(players);
//
//        for (int i = 0; i < CharacterHandler.instance.numberOfPlayers; i++)
//        {
//            players[i].transform.position = spawnpoints[i];
//            players[i].GetComponent<PlayerController>().enabled = true;
//            players[i].GetComponent<PlayerController>().cameraTrans = CameraMultiTarget.instance.GetComponent<Camera>().transform;
//        }
//
//        Debug.Log("Spawning in players");
//    }
//    
//}


//private IEnumerator testSwitchMat()
//{
//    for(int i = 0; i < 4; i++)
//    {
//        Debug.Log("Testing i = " + i);
//        renderer.material = playerMaterialList[i];
//
//        yield return new WaitForSeconds(3);
//    }
//}
//



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

