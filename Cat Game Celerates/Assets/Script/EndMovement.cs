using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMovement : MonoBehaviour
{
    private InteractiveMovement playerMovementScript;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovementScript = player.GetComponent<InteractiveMovement>();
        }
        else
        {
        }
    }

    void OnMouseDown()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.DisableMovement();
        }
    }
}
