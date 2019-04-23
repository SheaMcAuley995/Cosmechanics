using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public DialogueTrigger trigger;

    [Header("Text Objects")]
    //public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    Queue<string> sentences = new Queue<string>();

    [Header("Speed Settings")]
    public float letterPause = 0.1f;

	
    // Call this in DialogueTrigger whenever you wish to start the tutorial interaction
	public void StartDialogue(Dialogue dialogue)
    {
        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // Runs through the next inspector set dialogue
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    // Plays one letter at a time for each sentence
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            // Sound manager stuff

            yield return new WaitForSeconds(letterPause);
        }
    }

    // Self explanatory
    public void EndDialogue()
    {
        StopAllCoroutines();
    }
}
