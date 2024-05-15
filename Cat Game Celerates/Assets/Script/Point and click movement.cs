using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointandclickmovement : MonoBehaviour
{
    private Animator animator;
    public float speed = 5.0f; 
    private Vector3 ClickedPosition; 
    private bool isMoving = false;
    public bool IsMoving 
    {
        private set
        {
        if(isMoving != value)
            {
                isMoving = value; 
                animator.SetBool("Is_walking", value);
            }
        }
            get
        {
            return isMoving;
        }
    } 
    private bool canMove = true;

    void Start()
    {
        ClickedPosition = transform.position; 
        animator = GetComponent <Animator> ();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove) 
        {
            SetPosition();
        }

        if (IsMoving)
        {
            MoveCharacter();
        }
    }

    void SetPosition()
    {
        ClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ClickedPosition.z = transform.position.z; 
        IsMoving = true; 
    }

    void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, ClickedPosition, speed * Time.deltaTime);
        if (transform.position == ClickedPosition)
        {
            IsMoving = false;
        }
    }

    public void EnableMovement(bool enable)
     {
        canMove = enable;
        if (!canMove)
        {
            IsMoving = false;
        }
    }
}