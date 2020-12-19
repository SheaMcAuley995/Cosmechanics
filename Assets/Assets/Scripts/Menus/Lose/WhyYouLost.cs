using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhyYouLost : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI thisText;

    [Header("Lose Messages")]
    [TextArea(3, 10)] [SerializeField] string lostByDamageMessage;
    [TextArea(3, 10)] [SerializeField] string lostByFlorpMessage;

	
    void DisplayMessage()
    {
        switch (GameStateManager.instance.GetState())
        {
            case GameState.LostByDamage:
                thisText.text = lostByDamageMessage;
                break;
            case GameState.LostByFlorp:
                thisText.text = lostByFlorpMessage;
                break;
            default:
                thisText.text = " ";
                StartCoroutine(CheckForUpdatedState());
                break;
        }
    }
    
    // Use this for initialization
	void Awake()
    {
        DisplayMessage();
	}

    // In case for some reason there's a delay in updating the gameState
    IEnumerator CheckForUpdatedState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            DisplayMessage();
        }
    }
}
