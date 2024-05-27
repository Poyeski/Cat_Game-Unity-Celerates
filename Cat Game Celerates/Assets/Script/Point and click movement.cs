using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickMovement : MonoBehaviour
{
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    public float speed = 5.0f; 
    private Vector3 ClickedPosition; 
    private bool isMoving = false;
    private bool canMove = true;

    void Start()
    {
        ClickedPosition = transform.position; 
=======
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isMoving = false;
    private float screenBound;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector3 clickedPosition;
    private bool canMove = true;

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
        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        screenBound = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
        rb = GetComponent<Rigidbody2D>();
        clickedPosition = transform.position;
>>>>>>> Stashed changes
=======
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    private bool isMoving = false;
    private float screenLeftBound;
    private float screenRightBound;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector3 clickedPosition;
    private bool canMove = true;

    // Additional margin for left and right boundaries
    public float boundaryMargin = 2.0f;

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

        // Calculate screen boundaries with margin
        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        screenLeftBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x + halfPlayerWidth - boundaryMargin;
        screenRightBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z)).x - halfPlayerWidth + boundaryMargin;
>>>>>>> Stashed changes
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            SetPosition();
        }

<<<<<<< Updated upstream
        if (isMoving)
=======
        // Clamp the position to prevent the character from moving out of screen bounds
        Vector3 clampedPosition = new Vector3(Mathf.Clamp(transform.position.x, -screenBound, screenBound), transform.position.y, transform.position.z);
        transform.position = clampedPosition;
    }

    void FixedUpdate()
    {
        if (IsMoving)
>>>>>>> Stashed changes
        {
            MoveCharacter();
        }

        // Clamp the position to prevent the character from moving out of screen bounds
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        ClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ClickedPosition.z = transform.position.z; 
        isMoving = true; 
=======
        clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPosition.z = transform.position.z;

        clickedPosition.x = Mathf.Clamp(clickedPosition.x, -screenBound, screenBound);

        if (clickedPosition.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (clickedPosition.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }

        IsMoving = true;
>>>>>>> Stashed changes
=======
        clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPosition.z = transform.position.z;

        // Clamp the clicked position within the screen bounds
        clickedPosition.x = Mathf.Clamp(clickedPosition.x, screenLeftBound, screenRightBound);

        // Flip the sprite based on direction
        Vector3 direction = clickedPosition - transform.position;
        if (direction.x != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }

        IsMoving = true;
>>>>>>> Stashed changes
    }

    void MoveCharacter()
    {
        Vector3 newPosition = Vector3.MoveTowards(rb.position, clickedPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, clickedPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
        if (!canMove)
        {
            isMoving = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Entered Boundary: " + collision.gameObject.name);
            IsMoving = false; // Stop movement
            clickedPosition = transform.position; // Reset target position
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
<<<<<<< Updated upstream
        Gizmos.DrawLine(new Vector3(-screenBound, Camera.main.orthographicSize, 0), new Vector3(-screenBound, -Camera.main.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(screenBound, Camera.main.orthographicSize, 0), new Vector3(screenBound, -Camera.main.orthographicSize, 0));
=======
        Gizmos.DrawLine(new Vector3(screenLeftBound, Camera.main.orthographicSize, 0), new Vector3(screenLeftBound, -Camera.main.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(screenRightBound, Camera.main.orthographicSize, 0), new Vector3(screenRightBound, -Camera.main.orthographicSize, 0));
>>>>>>> Stashed changes
    }
}
