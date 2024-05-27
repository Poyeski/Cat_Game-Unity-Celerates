using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostInteractionWarp : MonoBehaviour
{
    public Vector3 warpPosition;

    private void Start()
    {
    }

    public void WarpPlayer()
    {
        InteractiveMovement player = FindObjectOfType<InteractiveMovement>();
        if (player != null)
        {
            player.transform.position = warpPosition;
            player.isInteracting = false;
            player.canMove = true;
        }
    }
}