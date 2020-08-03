using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;

public class LevisSceneTransition : MonoBehaviour
{
    public new LevisTransitionCamera camera;
    TextMeshProUGUI buttonText;

    Player player;

    string sceneName;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Start()
    {
        player.AddInputEventDelegate(OnSelectPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Select");

        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        sceneName = camera.target.name;
        buttonText.text = "Load " + sceneName;
    }

    void OnSelectPressed(InputActionEventData data)
    {
        if(camera.worldSelected)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }


}
