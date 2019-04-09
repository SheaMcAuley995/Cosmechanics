using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public PlayerController[] players;

    private void Awake()
    {
        players = FindObjectsOfType<PlayerController>();
    }

    public void LaunchLevel(string scene)
    {
        if (OverworldManager.instance.level == OverworldManager.Level.Level1)
        {
            scene = "NewMichaelTest";
        }
        else if (OverworldManager.instance.level == OverworldManager.Level.Level2)
        {
            scene = "NewMichaelTest";
        }
        else if (OverworldManager.instance.level == OverworldManager.Level.Level3)
        {
            scene = "NewMichaelTest";
        }

        SceneManager.LoadScene(scene);
    }
}
