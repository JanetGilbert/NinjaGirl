using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    float moveX;
    bool grounded;
    bool jumping;

    float jumpTime;


    public float speed = 1.0f;
    public float jumpspeed = 1.0f;
    public float maxJumpTime = 1.0f;

    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer sprite;

    // Changed this to be awake so that nothing tries to work before these are set
    void Awake()
    {
        // Get component references
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Is platform below us?
        float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;    // Figure out how far below us we need to start the raycast for ground check so it doesn't collide with the player hit box
        float rayLen =  0.05f;  // Distance to raycast
        Vector2 pos2D = (Vector2)transform.position;    // Player position
        // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen); // Try the raycast

        // This assumes we are not grounded and then sets grounded to true if the raycast hit a platform.
        grounded = false;
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                grounded = true;
            }
        }

        // Jumping
        if (grounded)   // If we are on the ground
        {
            moveX = Input.GetAxis("Horizontal");    // Set our horizontal movement equal to the horizontal axis

            if (jumpTime < 0.0f)    // If jump time is less than 0 we are not jumping
            {
                jumping = false;
            }
        }


        bool jumpPressed = Input.GetButtonDown("Jump");     // jumpPressed is set to true the frame we press the jump button and false all other times.
        if (grounded && jumpPressed && !jumping)    // If we are grounded & the jump button was pressed & we are not currently jumping we jump.
        {
            jumping = true;
            jumpTime = maxJumpTime; // Start jump timer
        }

        jumpTime -= Time.deltaTime;     // Decrease jump time

        // Reverse horizontal 
        float smallMove = 0.0001f;
        if (moveX < -smallMove) // If we are moving left we set flip the sprite to the left

        {
            sprite.flipX = true;
        }
        else if (moveX > smallMove) // If we are moving right we flip the sprite to the right
        {
            sprite.flipX = false;
        }

        // Animator parameters
        animator.SetFloat("speed", Mathf.Abs(moveX));
        animator.SetBool("grounded", grounded);
    }

    // Physic stuff
    private void FixedUpdate()
    {
        if (jumping)
        {
            if (jumpTime > 0.0f)
            {
                rb.velocity = new Vector2(moveX * speed, jumpspeed);
            }
        }
        else if (grounded)
        {
            rb.velocity = new Vector2(moveX * speed, 0.0f);
        }
    }
}
