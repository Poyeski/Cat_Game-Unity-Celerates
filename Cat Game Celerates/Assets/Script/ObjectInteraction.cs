using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public InteractiveMovement player;
    public Vector3 interactionPosition;
    public string interactionAnimation;

    void OnMouseDown()
    {
        player.InteractWithObject(interactionPosition, interactionAnimation);
    }
}