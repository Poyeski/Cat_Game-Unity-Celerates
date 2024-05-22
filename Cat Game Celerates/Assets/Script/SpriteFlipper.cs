using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    private float ScreenBound;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        ScreenBound = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
        rb = GetComponent<Rigidbody2D>();

        // Enable Gizmos for visual debugging
        Gizmos.color = Color.red;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePosition.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (mousePosition.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }
            Vector3 targetPosition = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime));
        }
        float clampedX = Mathf.Clamp(transform.position.x, -ScreenBound, ScreenBound);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Entered Boundary: " + collision.gameObject.name);
            rb.velocity = Vector2.zero;
        }
    }

    void OnDrawGizmos()
    {
        // Draw boundary Gizmos for visual debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-ScreenBound, Camera.main.orthographicSize, 0), new Vector3(-ScreenBound, -Camera.main.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(ScreenBound, Camera.main.orthographicSize, 0), new Vector3(ScreenBound, -Camera.main.orthographicSize, 0));
    }
}