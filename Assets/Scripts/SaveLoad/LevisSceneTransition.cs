using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;

public class LevisSceneTransition : MonoBehaviour
{
   public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
