using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// You can modify this script to create an event system of sorts for the tutorial, i.e. ending dialogue after a mechanic is explained, 
/// and then starting it back up after the mechanic is performed. 
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    [Space]
    public Dialogue dialogue;


	void Start ()
    {
        TriggerDialogue();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextDialogue();
        }
    }

    // Starts the dialogue with the player
    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue);
    }

    // Starts the next dialogue sentence with the player
    public void NextDialogue()
    {
        manager.DisplayNextSentence();
    }

    // Ends the current dialogue
    public void ExitDialogue()
    {
        manager.EndDialogue();
    }
}
