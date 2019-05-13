﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    CharToDestroy[] charsToDestroy;

    // TODO: Store user data on levels they've beaten & lock levels 2 and 3 until reached
    public void LaunchLevel(string scene)
    {
        charsToDestroy = FindObjectsOfType<CharToDestroy>();

        switch (OverworldManager.instance.level)
        {
            case OverworldManager.Level.Level1:
                scene = "Ship_Level_Tutorial NEW";
                break;
            case OverworldManager.Level.Level2:
                scene = "BetaMichaelTest";
                break;
            case OverworldManager.Level.Level3:
                scene = "Ship_Level_1Final";
                break;
        }

        for (int i = 0; i < PlayerActivation.instance.chosenCharacters.Length; i++)
        {
            PlayerActivation.instance.chosenCharacters[i].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        for (int j = 0; j < charsToDestroy.Length; j++)
        {
            Destroy(charsToDestroy[j].gameObject);
        }

        PlayerActivation.instance.chosenCharacters[0].GetLevelScene(scene);
        SceneFader.instance.FadeTo(scene);
    }
}