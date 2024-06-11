using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Transform playArea; // Reference to the play area for boundary checking

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get movement input from WASD or arrow keys
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
        ClampPosition();
    }

    void ClampPosition()
    {
        var bounds = playArea.GetComponent<Renderer>().bounds;
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x); // Adjust these values based on your play area
        pos.y = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y); // Adjust these values based on your play area
        transform.position = pos;
    }
}