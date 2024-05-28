using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string[] dialogueLines;
    public Vector3 interactionPosition;
    public string interactionAnimation;
    public Sprite dialogueImage; // Assign an image for the dialogue
    public int showImageAtIndex = -1; // Index at which to show the image

    private void OnMouseDown()
    {
        Debug.Log("Object clicked: " + gameObject.name);
        if (DialogueText.Instance != null && !DialogueText.Instance.isDialogueActive && dialogueLines.Length > 0)
        {
            DialogueText.Instance.StartDialogue(dialogueLines, dialogueImage, showImageAtIndex); // Pass the image and index to the dialogue
        }
        InteractiveMovement player = FindObjectOfType<InteractiveMovement>();
        if (player != null)
        {
            player.InteractWithObject(interactionPosition, interactionAnimation);
        }
    }
}