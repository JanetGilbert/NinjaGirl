using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Kate Howell
 * Original Script By: Janet Gilbert
 * 
 * In the revised version of this project, This class will functin as the Model in The MVC pattern
 * This class will store the Data and Logic for the Ninja Object
 */

public class Ninja : MonoBehaviour
{
    //grounded Property
    private bool grounded; //checks if player is collidng with a platform

    public bool isGrounded
    {
        get
        {
            return grounded;
        }

        set
        {
            grounded = value;
            myView.SetGrounded(grounded); //Update the View
        }
    }

    //private jumping logic
    bool jumping; //if player is currently jumping
    float jumpTime; //controls the time spent jumping

    //public movement logic variables
    [SerializeField, Tooltip("Speed on X Axis")]
    public float speed = 1.0f;
    [SerializeField, Tooltip("Jump Speed on Y Axis")]
    public float jumpspeed = 1.0f;
    [SerializeField, Tooltip("Max time the Ninja can remain Jumping")]
    public float maxJumpTime = 1.0f;

    //linked components
    View myView; 
    Controller myController;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;



    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    //returns true if player is grounded
    public bool CheckGrounded()
    {
        //Check for platform under player using a raycast
        float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
        float rayLen = 0.05f;
        Vector2 pos2D = (Vector2)transform.position;
        // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                return true; //set grounded to true if a platform is hit
            }
        }

        return false;
    }

    public void Move(float moveX, bool jumpPressed)
    {
        grounded = CheckGrounded();

        if (grounded && jumpPressed && !jumping)
        {
            jumping = true;
            jumpTime = maxJumpTime;
        }

        jumpTime -= Time.deltaTime;

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

        //if jump time has reached 0, stop applying the jump force
        if (jumpTime < 0.0f && grounded)
        {
            jumping = false;
        }
    }

    //Set view parameter of Model
    public void SetView(View view)
    {
        myView = view;
    }

    //set input parameter of Model
    public void SetInput(Controller controller)
    {
        myController = controller;
    }

}
