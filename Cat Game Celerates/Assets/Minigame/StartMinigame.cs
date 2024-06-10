using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMinigame : MonoBehaviour
{
    public string minigameOverlaySceneName = "MinigameOverlay"; 
    public string returnSceneName; 
    public int mouseButtonIndex = 0; 

    void OnMouseDown()
    {
        Minigame();
    }


    public void Minigame()
    {

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.returnSceneName = returnSceneName;
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        // Load the minigame overlay scene
        if (!string.IsNullOrEmpty(minigameOverlaySceneName))
        {
            SceneManager.LoadScene(minigameOverlaySceneName);
        }
        else
        {
            Debug.LogError("Minigame overlay scene name is not specified.");
        }
    }
}