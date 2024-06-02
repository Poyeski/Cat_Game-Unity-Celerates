using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Play button (start tutorial level)
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    } 

    public void QuitGame()
    {
        Application.Quit();
    }
}
