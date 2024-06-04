using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public InteractionAnimation interactionAnimation; 
    public string nextSceneName;

    private void OnMouseDown()
    {
        if (interactionAnimation != null && interactionAnimation.onDrawer)
        {
            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        // Adjust this delay if needed
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }
}
