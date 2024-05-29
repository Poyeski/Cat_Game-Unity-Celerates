using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isMoving = false;
    public bool isInteracting = false;
    public bool canMove = true;
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    

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

        // if (IsMoving)
        // {
            // MoveCharacter();
        // }
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
        targetPosition = new Vector3(position.x, position.y, transform.position.z);
        Vector3 direction = targetPosition - transform.position;
        if (direction.x != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        IsMoving = true;
         Debug.Log($"is moving value change {IsMoving} by SetTargetPosition", this);
    }

    void MoveCharacter()
    {
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, targetPosition) < 2f)
        {
            IsMoving = false;
           
            Debug.Log($"is moving value change {IsMoving} by MoveCharacter", this);
            
        }
    }

    public void InteractWithObject(Vector3 interactionPosition, string interactionAnimation)
    {
        canMove = false;
        isInteracting = true;
        targetPosition = interactionPosition;
        SetTargetPosition(interactionPosition);
        //StartCoroutine(PerformInteraction(interactionAnimation));
    }

    private IEnumerator PerformInteraction(string interactionAnimation)
    {
        while (IsMoving)
        {
            yield return null;
        }
        animator.Play(interactionAnimation);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isInteracting = false;
        canMove = true;
        PostInteractionWarp warpScript = GetComponent<PostInteractionWarp>();
        if (warpScript != null)
        {
            warpScript.WarpPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Entered Boundary: " + collision.gameObject.name);
            IsMoving = false;
            targetPosition = transform.position;

             Debug.Log($"is moving value change {IsMoving} by OnTriggerEnter2D", this);
        }
    }
}
