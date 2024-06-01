using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionAnimator : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        Vector3 playerPosition = transform.position;
        animator.SetFloat("PlayerPosX", playerPosition.x);
        animator.SetFloat("PlayerPosY", playerPosition.y);
        animator.SetFloat("PlayerPosZ", playerPosition.z);
    }
}
