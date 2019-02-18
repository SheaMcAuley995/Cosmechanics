using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject characterSelectionPanel;

    public void StartGame()
    {
        characterSelectionPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
