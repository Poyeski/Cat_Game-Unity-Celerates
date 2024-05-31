using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    private Vector3 currentPlayerPosition;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void UpdatePlayerPosition()
    {
        currentPlayerPosition = transform.position;
        animator.SetFloat("PlayerPosX", currentPlayerPosition.x);
        animator.SetFloat("PlayerPosY", currentPlayerPosition.y);
        animator.SetFloat("PlayerPosZ", currentPlayerPosition.z);
    }
}
