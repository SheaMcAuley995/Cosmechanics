using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SelectedPlayer : MonoBehaviour
{
    [HideInInspector] public string currentScene;

	public void GetLevelScene(string scene)
    {
        currentScene = scene;
    }
}
