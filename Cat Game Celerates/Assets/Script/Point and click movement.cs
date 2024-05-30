using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool canMove = true;
    private bool isMoving = false;
    private float screenLeftBound;
    private float screenRightBound;
    public float boundaryMargin = 2.0f;
    public float moveSpeed = 5f;
    private Vector3 clickedPosition;
    private bool isWithinBoundary = false;

    public bool IsMoving
    {
        set
        {
            if (isMoving != value)
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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        clickedPosition = transform.position;
        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        screenLeftBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x + halfPlayerWidth - boundaryMargin;
        screenRightBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z)).x - halfPlayerWidth + boundaryMargin;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            SetPosition();
        }

        if (isWithinBoundary)
        {
            Debug.Log("Player is within the boundary");
            IsMoving = false;
        }
        else
        {
            Debug.Log("Player is outside the boundary");
            IsMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            MoveCharacter();
        }
    }

    void SetPosition()
    {
        clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPosition.z = transform.position.z;
        clickedPosition.x = Mathf.Clamp(clickedPosition.x, screenLeftBound, screenRightBound);
        Vector3 direction = clickedPosition - transform.position;
        if (direction.x != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }

        IsMoving = true;
        Debug.Log($"is moving value change {IsMoving} by SetPosition", this);
    }

    void MoveCharacter()
    {
        Vector3 newPosition = Vector3.MoveTowards(rb.position, clickedPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        Debug.Log(Vector3.Distance(rb.position, clickedPosition));

        if (Vector3.Distance(rb.position, clickedPosition) < 2f)
        {
            IsMoving = false;
            Debug.Log($"is moving value change {IsMoving} by MoveCharacter", this);
        }
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
        if (!canMove)
        {
            IsMoving = false;
            Debug.Log($"is moving value change {IsMoving} by EnableMovement", this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Entered Boundary: " + collision.gameObject.name);
            isWithinBoundary = true;
            IsMoving = false;
            Debug.Log($"is moving value change {IsMoving} by OnTriggerEnter2D", this);
            clickedPosition = transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            isWithinBoundary = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(screenLeftBound, Camera.main.orthographicSize, 0), new Vector3(screenLeftBound, -Camera.main.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(screenRightBound, Camera.main.orthographicSize, 0), new Vector3(screenRightBound, -Camera.main.orthographicSize, 0));
    }
}
