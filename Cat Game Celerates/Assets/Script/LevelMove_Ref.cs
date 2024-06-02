using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;

    //change scene using collider. If player enter collider area, change scene

    //other.GetComponent<Player>()
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        if(other.tag == "Player")
        {
            print ("Switching scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
