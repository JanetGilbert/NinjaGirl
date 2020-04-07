using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    //Direction of movement input on the x-axis
    float moveX;

    //Tracks whether or not the player is on the ground
    bool grounded;

    //Tracks whether or not the player is jumping upwards 
    bool jumping;

    //Tracks how much longer the player can ascend while jumping before falling back down
    float jumpTime;


    //Horizontal movement speed
    public float speed = 1.0f;

    //Initial vertical movement speed at the start of a jump
    public float jumpSpeed = 1.0f;

    //Max time the player stays in the air during a jump
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

    void Update()
    {
        // Determines distance between ninja and ground
        float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
        float rayLen =  0.05f;
        Vector2 pos2D = (Vector2)transform.position;
        // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

        //The "grounded" boolean defaults to false, assuming the ninja is off the ground
        grounded = false;

        //Determines whether or not the ninja is actually off the ground
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                //In this circumstance, the ninja is in fact not off the ground.
                grounded = true;
            }
        }

        //Ninja cannot jump or change direction while not on the ground
            //(Ninja is not a very good ninja by cartoon ninja standards)
        if (grounded)
        {
            //Reads horizontal control input
            moveX = Input.GetAxis("Horizontal");

            //Determines whether the ninja is still jumping
            if (jumpTime < 0.0f)
            {
                jumping = false;
            }
        }

        //Determines when the ninja jumps
        if (grounded && Input.GetButtonDown("Jump") && !jumping)
        {
            jumping = true;
            jumpTime = maxJumpTime;
        }

        //Counts how long until the ninja can jump again
        jumpTime -= Time.deltaTime;

        // Reverses sprite orientation based on horizontal movement
        float smallMove = 0.0001f;
        if (moveX < -smallMove)
        {
            sprite.flipX = true;
        }
        else if (moveX > smallMove)
        {
            sprite.flipX = false;
        }

        // Sets animation parameters
        animator.SetFloat("speed", Mathf.Abs(moveX));
        animator.SetBool("grounded", grounded);
    }

    // Performs 'physic' stuff
        //(Because I'm not about to let that minor misspelling of "physics" go unnoticed)
    private void FixedUpdate()
    {
        //Sets initial horizontal and vertical velocity
            //(Ninja jumps off ground and maintains horizontal velocity while gravity reduces vertical velocity) 
        if (jumping)
        {
            if (jumpTime > 0.0f)
            {
                rb.velocity = new Vector2(moveX * speed, jumpSpeed);
            }
        }

        //Maintains horizontal velocity
        else if (grounded)
        {
            rb.velocity = new Vector2(moveX * speed, 0.0f);
        }
    }
}
