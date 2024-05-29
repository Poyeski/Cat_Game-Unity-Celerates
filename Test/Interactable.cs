using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool kucing_masuk=false;//new alif
    public string[] dialogueLines;

    private void OnMouseDown()
    {
        Debug.Log("Object clicked: " + gameObject.name);
        if (DialogueText.Instance != null && !DialogueText.Instance.isDialogueActive && dialogueLines.Length > 0 && kucing_masuk) //new alif
        {
            DialogueText.Instance.StartDialogue(dialogueLines);

        }
    }
   void OnTriggerEnter2D(Collider2D col) // new Alif
   {
         Debug.Log("kucing masuk");
         kucing_masuk = true;
   }

   void OnTriggerExit2D(Collider2D col)
   {
        Debug.Log("kucing keluar");
        kucing_masuk = false;
   }
}
