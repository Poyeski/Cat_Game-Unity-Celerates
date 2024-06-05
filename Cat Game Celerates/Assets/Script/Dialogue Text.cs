using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueText : MonoBehaviour
{
    public static DialogueText Instance { get; private set; }
    public Canvas dialogueCanvas; // Reference to the Canvas instead of just TextMeshProUGUI
    public TextMeshProUGUI dialogueText;
    public Image overlayImage;
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
        if (dialogueCanvas!=null)
        {
            dialogueCanvas.gameObject.SetActive(false);
        }
        

        Debug.Log("Dialogue canvas set to inactive in Awake.");
    }

    void Start()
    {
        overlayImage.gameObject.SetActive(false);
        Debug.Log("Overlay image set to inactive in Start.");
        SceneManager.sceneLoaded += GetDailogueBox; //setiap kali event sceneLoaded dijalankan, akan menjalankan getdialoguebox
    }

    private void GetDailogueBox(Scene var,LoadSceneMode load)
    {
        if (dialogueCanvas==null)
        {
           GameObject.Find("DialogueBox");
        }

         if (dialogueCanvas!=null)
        {
            dialogueCanvas.gameObject.SetActive(false);
        }
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

        overlayImage.gameObject.SetActive(false);

        dialogueCanvas.gameObject.SetActive(true);
        Debug.Log("Dialogue canvas set to active.");
        DisplayNextSentence();
        scriptInteractiveMovement.canMove = false;
        scriptInteractiveMovement.isInteracting = true;
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
        dialogueCanvas.gameObject.SetActive(false);
        Debug.Log("Dialogue canvas set to inactive.");
        playerController.EnableMovement(true);
        isDialogueActive = false;
        overlayImage.gameObject.SetActive(false);
        scriptInteractiveMovement.canMove = true;
        scriptInteractiveMovement.isInteracting = false;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) && dialogueCanvas.gameObject.activeSelf) ||
            (Input.GetKeyDown(KeyCode.Space) && dialogueCanvas.gameObject.activeSelf))
        {
            DisplayNextSentence();
            Debug.Log("Mouse click or space pressed. Displaying next sentence.");
        }
    }



}