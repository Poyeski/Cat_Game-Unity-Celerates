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

    public bool IsMoving
    {
        private set
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

        if (IsMoving)
        {
            MoveCharacter();
        }
        Vector3 clampedPosition = new Vector3(Mathf.Clamp(transform.position.x, screenLeftBound, screenRightBound), transform.position.y, transform.position.z);
        transform.position = clampedPosition;
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
    }

    void MoveCharacter()
    {
        // Determine direction
        Vector3 direction = clickedPosition - transform.position;

        // If direction is not zero, flip the character
        if (direction.x != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }

        transform.position = Vector3.MoveTowards(transform.position, clickedPosition, moveSpeed * Time.deltaTime);

        if (transform.position == clickedPosition)
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Entered Boundary: " + collision.gameObject.name);
            IsMoving = false;
            clickedPosition = transform.position;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(screenLeftBound, Camera.main.orthographicSize, 0), new Vector3(screenLeftBound, -Camera.main.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(screenRightBound, Camera.main.orthographicSize, 0), new Vector3(screenRightBound, -Camera.main.orthographicSize, 0));
    }
}
