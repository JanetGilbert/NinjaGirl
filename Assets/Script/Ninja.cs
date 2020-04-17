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

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Is platform below us?
        float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
        float rayLen =  0.05f;
        Vector2 pos2D = (Vector2)transform.position;
       // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

        grounded = false;
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                grounded = true;
            }
        }

        // Jumping
        if (grounded)
        {
            moveX = Input.GetAxis("Horizontal");


            if (jumpTime < 0.0f)
            {
                jumping = false;
            }
        }


        bool jumpPressed = Input.GetButtonDown("Jump");
        if (grounded && jumpPressed && !jumping)
        {
            jumping = true;
            jumpTime = maxJumpTime;
        }

        jumpTime -= Time.deltaTime;

        // Reverse horizontal 
        float smallMove = 0.0001f;
        if (moveX < -smallMove)
        {
            sprite.flipX = true;
        }
        else if (moveX > smallMove)
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
