using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public Queue<string> sentences;

	void Start () {
        sentences = new Queue<string>();
	}

    public void startDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach(string sentence in sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
    }

    private void EndDialogue()
    {
        throw new NotImplementedException();
    }
}
