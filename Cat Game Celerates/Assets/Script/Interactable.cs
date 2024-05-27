using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string[] dialogueLines;
    public Vector3 interactionPosition; // Position where the player should move to interact
    public string interactionAnimation; // Animation to play when interacting

    private void OnMouseDown()
    {
        Debug.Log("Object clicked: " + gameObject.name);
        if (DialogueText.Instance != null && !DialogueText.Instance.isDialogueActive && dialogueLines.Length > 0)
        {
            DialogueText.Instance.StartDialogue(dialogueLines);
        }

        // Get the player and trigger interaction
        InteractiveMovement player = FindObjectOfType<InteractiveMovement>();
        if (player != null)
        {
            player.InteractWithObject(interactionPosition, interactionAnimation);
        }
    }
}
