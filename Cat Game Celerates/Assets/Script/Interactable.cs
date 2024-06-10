using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string[] dialogueLines;
    public Vector3 interactionPosition;
    public string interactionAnimation;
    public Sprite dialogueImage;
    public int showImageAtIndex = -1;
    //public bool isDialogue = false;
    //public DialogueText scriptDialogueText;

    //void Start()
    // {
    // scriptDialogueText = FindObjectOfType(DialogueText);

    // }

    private void OnMouseDown()
    {
        if (DialogueText.Instance != null && !DialogueText.Instance.isDialogueActive && dialogueLines.Length > 0)
        {
            //isDialogue = true;
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