using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueText : MonoBehaviour
{
    public static DialogueText Instance { get; private set; } 
    public GameObject dialogueBox; 
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    public PointAndClickMovement playerController;
    public bool isDialogueActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
    }
    public void StartDialogue(string[] lines)
    {
        if (isDialogueActive) return; 
        isDialogueActive = true;
        playerController.EnableMovement(false);
        Debug.Log("Starting dialogue with " + lines.Length + " lines.");
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
            Debug.Log("Enqueued line: " + line);
        }

        dialogueBox.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log("Displaying next sentence. Remaining: " + sentences.Count);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log("Current sentence: " + sentence);
        dialogueText.text = sentence; 
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        playerController.EnableMovement(true);
        isDialogueActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && dialogueBox.activeSelf)
        {
            DisplayNextSentence();
        }
    }
}
