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

    void Start()
    {
        overlayImage.gameObject.SetActive(false); // Hide the overlay image at the start
    }

    public void StartDialogue(string[] lines, Sprite image = null, int showImageAtIndex = -1)
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

        imageToShow = image;
        imageShowIndex = showImageAtIndex;
        currentSentenceIndex = 0;

        overlayImage.gameObject.SetActive(false); // Ensure the overlay image is hidden initially

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

        Debug.Log("Current sentence index: " + currentSentenceIndex);
        if (currentSentenceIndex == imageShowIndex && imageToShow != null)
        {
            overlayImage.sprite = imageToShow;
            overlayImage.gameObject.SetActive(true);
            Debug.Log("Displaying image: " + imageToShow.name);
        }

        string sentence = sentences.Dequeue();
        Debug.Log("Current sentence: " + sentence);
        dialogueText.text = sentence;
        currentSentenceIndex++;
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        playerController.EnableMovement(true);
        isDialogueActive = false;
        overlayImage.gameObject.SetActive(false); // Hide the image when dialogue ends
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && dialogueBox.activeSelf)
        {
            DisplayNextSentence();
        }
    }
}
