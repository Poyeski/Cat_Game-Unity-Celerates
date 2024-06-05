using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratsMenu : MonoBehaviour
{
     public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);

    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}
