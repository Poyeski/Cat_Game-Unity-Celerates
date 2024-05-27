using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    private bool isMoving = false;
    private bool isInteracting = false;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
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
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (canMove && !isInteracting && Input.GetMouseButtonDown(0))
        {
            SetTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (IsMoving)
        {
            MoveCharacter();
        }
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            MoveCharacter();
        }
    }

    void SetTargetPosition(Vector3 position)
    {
        targetPosition = new Vector3(position.x, transform.position.y, transform.position.z); // Linear path constraint

        // Flip the sprite based on direction
        Vector3 direction = targetPosition - transform.position;
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
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
        {
            IsMoving = false;
        }
    }

    public void InteractWithObject(Vector3 interactionPosition, string interactionAnimation)
    {
        canMove = false;
        isInteracting = true;
        targetPosition = interactionPosition;
        SetTargetPosition(interactionPosition);
        StartCoroutine(PerformInteraction(interactionAnimation));
    }

    private IEnumerator PerformInteraction(string interactionAnimation)
    {
        // Move to interaction position
        while (IsMoving)
        {
            yield return null;
        }

        // Play interaction animation
        animator.Play(interactionAnimation);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Interaction completed
        isInteracting = false;
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Entered Boundary: " + collision.gameObject.name);
            IsMoving = false; // Stop movement
            targetPosition = transform.position; // Reset target position
        }
    }

    void OnDrawGizmos()
    {
        // Draw a line for visual debugging
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, targetPosition);
    }
}