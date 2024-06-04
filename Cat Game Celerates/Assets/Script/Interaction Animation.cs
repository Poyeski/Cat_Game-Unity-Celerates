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
    public bool onFloor = true;
    public bool shelfClicked = false;
    public float resetDelay = 0.1f;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Shelf"))
        {
            OnObjectClicked();
        }
        if (gameObject.CompareTag("Drawer"))
        {
            OnObjectClickedDrawer();
        }
        if (gameObject.CompareTag("Floor"))
        {
            OnObjectClickedFloor();
        }
    }

    public void OnObjectClickedDrawer()
    {
        animator.SetTrigger("JumpFromShelf");
        StartCoroutine(ResetTriggerDrawer());
    }
    public void OnObjectClickedFloor()
    {
        animator.SetTrigger("JumpFromDrawer");
        StartCoroutine(ResetTrigger());
    }
    public void OnObjectClicked()
    {
        animator.SetTrigger("JumpShelf");
    }

    System.Collections.IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(resetDelay);
        animator.ResetTrigger("JumpFromDrawer");
    }

    System.Collections.IEnumerator ResetTriggerDrawer()
    {
        yield return new WaitForSeconds(resetDelay);
        animator.ResetTrigger("JumpFromShelf");
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
            onFloor = false;
            animator.SetTrigger("DrawerTrigger");
            animator.SetBool("onDrawer", true);
            animator.SetBool("onShelf", false);
            animator.SetBool("onFloor", false);
        }
        if (collision.CompareTag("Shelf"))
        {
            onShelf = true;
            onDrawer = false;
            animator.SetBool("onShelf", true);
            animator.SetBool("onDrawer", false);
        }
        if (collision.CompareTag("Floor"))
        {
            onShelf = false;
            onDrawer = false;
            onFloor= true;
            animator.SetBool("onShelf", false);
            animator.SetBool("onDrawer", false);
            animator.SetBool("onFloor", true);
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
}