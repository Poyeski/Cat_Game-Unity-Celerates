using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string[] dialogueLines;

    private void OnMouseDown()
    {
        Debug.Log("Object clicked: " + gameObject.name);
        if (DialogueText.Instance != null && !DialogueText.Instance.isDialogueActive && dialogueLines.Length > 0)
        {
            DialogueText.Instance.StartDialogue(dialogueLines);
        }
    }
}
