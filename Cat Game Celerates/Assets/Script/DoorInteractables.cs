using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractables : MonoBehaviour
{
    public string[] dialogueLines;
    public Vector3 interactionPosition;
    public string interactionAnimation;
    public Sprite dialogueImage;
    public int showImageAtIndex = -1;
    public InteractionAnimation interactionAnimationScript;

    private void Start()
    {
        if (interactionAnimationScript == null)
        {
            interactionAnimationScript = FindObjectOfType<InteractionAnimation>();
        }
    }

    private void OnMouseDown()
    {
        if (interactionAnimationScript != null && !interactionAnimationScript.onDrawer && interactionAnimationScript.onFloor)
        {
            if (DialogueText.Instance != null && !DialogueText.Instance.isDialogueActive && dialogueLines.Length > 0)
            {
                Debug.Log("Object clicked: " + gameObject.name);
                DialogueText.Instance.StartDialogue(dialogueLines, dialogueImage, showImageAtIndex);
            }

            InteractiveMovement player = FindObjectOfType<InteractiveMovement>();
            if (player != null)
            {
                player.InteractWithObject(interactionPosition, interactionAnimation);
            }
        }
    }
}