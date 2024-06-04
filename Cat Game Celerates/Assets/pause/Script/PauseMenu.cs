using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Method to pause the game
    public void Pause()
    {
        Time.timeScale = 0f;
        Debug.Log("test");
    }

    // Method to unpause the game
    public void Unpause()
    {
        Time.timeScale = 1f;
    }
}
