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
    public Image overlayImage; // Reference the image component
    private Queue<string> sentences;
    public PointAndClickMovement playerController;
    public bool isDialogueActive = false;
    private Sprite imageToShow;
    private int imageShowIndex;
    private int currentSentenceIndex;
    public InteractiveMovement scriptInteractiveMovement;


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

        if (dialogueBox == null)
        {
            Debug.LogError("dialogueBox is not assigned in the inspector.");
        }
        else
        {
            dialogueBox.SetActive(false);
        }

        if (dialogueText == null)
        {
            Debug.LogError("dialogueText is not assigned in the inspector.");
        }

        if (overlayImage == null)
        {
            Debug.LogError("overlayImage is not assigned in the inspector.");
        }

        if (playerController == null)
        {
            Debug.LogError("playerController is not assigned in the inspector.");
        }

        if (scriptInteractiveMovement == null)
        {
            Debug.LogError("scriptInteractiveMovement is not assigned in the inspector.");
        }
    }

    void Start()
    {
        if (overlayImage != null)
        {
            overlayImage.gameObject.SetActive(false); // Hide the overlay image at the start
        }
    }


        public void StartDialogue(string[] lines, Sprite image = null, int showImageAtIndex = -1)
    {
        if (isDialogueActive) return;
        isDialogueActive = true;

        if (playerController != null)
        {
            playerController.EnableMovement(false);
        }

        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        imageToShow = image;
        imageShowIndex = showImageAtIndex;
        currentSentenceIndex = 0;

        if (overlayImage != null)
        {
            overlayImage.gameObject.SetActive(false);
        }

        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
        }

        DisplayNextSentence();

        if (scriptInteractiveMovement != null)
        {
            scriptInteractiveMovement.canMove = false;
            scriptInteractiveMovement.isInteracting = true;
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (currentSentenceIndex == imageShowIndex && imageToShow != null && overlayImage != null)
        {
            overlayImage.sprite = imageToShow;
            overlayImage.gameObject.SetActive(true);
        }

        string sentence = sentences.Dequeue();
        if (dialogueText != null)
        {
            dialogueText.text = sentence;
        }

        currentSentenceIndex++;
    }

    public void EndDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.EnableMovement(true);
        }

        isDialogueActive = false;

        if (overlayImage != null)
        {
            overlayImage.gameObject.SetActive(false);
        }

        if (scriptInteractiveMovement != null)
        {
            scriptInteractiveMovement.canMove = true;
            scriptInteractiveMovement.isInteracting = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && dialogueBox.activeSelf || Input.GetKeyDown(KeyCode.Space) && dialogueBox.activeSelf )
        {
            DisplayNextSentence();

        }
    }
}
