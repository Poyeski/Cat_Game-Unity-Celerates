using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAnimation : MonoBehaviour
{
    public Animator animator;
    public string PlayerAnimation;
    public string boolParameterName = "IsScratching";
    public bool onDrawer = false;
    public bool onShelf = false;
    public bool shelfClicked = false;

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
            onDrawer = true;
            onShelf = false; 
            animator.SetTrigger("DrawerTrigger");
            animator.SetBool("onDrawer", true);
            animator.SetBool("onShelf", false);
        }
        if (collision.CompareTag("Shelf"))
        {
            onShelf = true;
            onDrawer = false;
            animator.SetBool("onShelf", true);
            animator.SetBool("onDrawer", false);
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CatTower"))
        {
            animator.SetBool(boolParameterName, false);
        }
        if (collision.CompareTag("Shelf"))
        {
            onShelf = false;
            onDrawer = true;
            animator.SetBool("onShelf", false);
            animator.SetBool("onDrawer", true);
        }
    }

    public void OnObjectClicked()
    {
        animator.SetTrigger("JumpShelf");
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Shelf"))
        {
            OnObjectClicked();
            Debug.Log("Shelf clicked but onDrawer is false, ShelfTrigger not triggered");
        }
    }
}