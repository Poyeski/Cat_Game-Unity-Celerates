using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointandclickmovement : MonoBehaviour
{
    public float speed = 5.0f; 
    private Vector3 ClickedPosition; 
    private bool isMoving = false;
    private bool canMove = true;

    void Start()
    {
        ClickedPosition = transform.position; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove) 
        {
            SetPosition();
        }

        if (isMoving)
        {
            MoveCharacter();
        }
    }

    void SetPosition()
    {
        ClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ClickedPosition.z = transform.position.z; 
        isMoving = true; 
    }

    void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, ClickedPosition, speed * Time.deltaTime);
        if (transform.position == ClickedPosition)
        {
            isMoving = false;
        }
    }

    public void EnableMovement(bool enable)
     {
        canMove = enable;
        if (!canMove)
        {
            isMoving = false;
        }
    }
}