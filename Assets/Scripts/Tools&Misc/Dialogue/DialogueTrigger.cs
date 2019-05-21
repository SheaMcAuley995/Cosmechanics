using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    [Space]
    public Dialogue dialogue;


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
