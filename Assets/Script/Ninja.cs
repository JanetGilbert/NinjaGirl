using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    [HideInInspector] public float moveX;
    [HideInInspector] public bool grounded;
    bool jumping;

    float jumpTime;


    public float speed = 1.0f;
    public float jumpspeed = 1.0f;
    public float maxJumpTime = 1.0f;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    NinjaView ninjaView;

    void Start()
    {
        ninjaView = GetComponent<NinjaView>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();

        // Jumping 
        if (grounded)
        {

            if (jumpTime < 0.0f)
            {
                jumping = false;
            }
        }
        jumpTime -= Time.deltaTime;

        //Updates the animator based on current movement.
        ninjaView.UpdateAnimator(moveX, grounded);

    }

    void DetectGround()
    {
        // Is platform below us? Get raycast based on position directly below height of the player's box collider
        float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
        float rayLen = 0.05f;
        Vector2 pos2D = (Vector2)transform.position;
        // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

        grounded = false;
        //If the raycast is touching the platform, then character is grounded and stop gravity from occuring.
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                grounded = true;
            }
        }
    }

    public void Jump()
    {
        if (grounded && !jumping)
        {
            jumping = true;
            jumpTime = maxJumpTime;
        }
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
