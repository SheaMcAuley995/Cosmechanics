using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
	public void LaunchLevel(string scene)
    {
        if (OverworldManager.instance.level == OverworldManager.Level.Level1)
        {
            scene = "CopyOfMainMAp";
            SceneManager.LoadScene(scene);
        }
        else if (OverworldManager.instance.level == OverworldManager.Level.Level2)
        {
            scene = "NewMichaelTest";
            SceneManager.LoadScene(scene);
        }
        else if (OverworldManager.instance.level == OverworldManager.Level.Level3)
        {
            scene = "ZachNewMichaelTest";
            SceneManager.LoadScene(scene);
        }
    }
}
