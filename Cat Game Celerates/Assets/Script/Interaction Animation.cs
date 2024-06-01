using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAnimation : MonoBehaviour
{
    public Animator animator;
    public string PlayerAnimation;
    public string boolParameterName = "IsScratching";

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CatTower"))
        {
            animator.SetBool(boolParameterName, true);
        }
        if (collision.CompareTag("Drawer"))
        {
            animator.SetTrigger("DrawerTrigger");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CatTower"))
        {
            animator.SetBool(boolParameterName, false);
        }
    }
}