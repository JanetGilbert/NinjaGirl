using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    float moveX; //X axis Input
    bool grounded; //checks if player is collidng with a platform
    bool jumping; //checks if player is currently jumping

    float jumpTime; //used to control the time spent jumping

    [SerializeField, Tooltip("Speed on X Axis")]
    public float speed = 1.0f;
    [SerializeField, Tooltip("Jump Speed on Y Axis")]
    public float jumpspeed = 1.0f;
    [SerializeField, Tooltip("Max time the Ninja can remain Jumping")]
    public float maxJumpTime = 1.0f;

    //Ninja Object Components
    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer sprite;

    void Start()
    {
        //get instances of each necessary component
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for platform under player using a raycast
        float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
        float rayLen =  0.05f;
        Vector2 pos2D = (Vector2)transform.position;
       // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

        //set grounded to false every frame
        grounded = false;
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                grounded = true; //set grounded to true if a platform is hit
            }
        }

        //Input Management
        if (grounded)
        {
            moveX = Input.GetAxis("Horizontal"); // get X Axis input 

            //if jump time has reached 0, stop applying the jump force
            if (jumpTime < 0.0f)
            {
                jumping = false;
            }
        }

        //get jump input
        bool jumpPressed = Input.GetButtonDown("Jump");
        if (grounded && jumpPressed && !jumping)
        {
            jumping = true;
            jumpTime = maxJumpTime;
        }

        jumpTime -= Time.deltaTime;

        //Sprite Direction based off minimam movement
        float smallMove = 0.0001f;
        if (moveX < -smallMove)
        {
            sprite.flipX = true;
        }
        else if (moveX > smallMove)
        {
            sprite.flipX = false;
        }

        //Set Animator Vars
        animator.SetFloat("speed", Mathf.Abs(moveX));
        animator.SetBool("grounded", grounded);
    }

    
    //Fixed Update is used for in Game Physics
    private void FixedUpdate()
    {
        if (jumping)
        {
            if (jumpTime > 0.0f)
            {
                //if jump time has not reached 0, set the players Y veloctiy to jumpspeed
                rb.velocity = new Vector2(moveX * speed, jumpspeed);
            }
        }
        else if (grounded)
        {
            //set the X velocity to X axis speed mutiplied by the X axis input
            rb.velocity = new Vector2(moveX * speed, 0.0f);
        }
    }
}
