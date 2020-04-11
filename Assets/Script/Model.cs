using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the Model script of the MVC
public class Model 
{
    //access to the View
    public View View;
    // access to the Manager 
    public Manager Manager;

    public bool grounded; // check is the player on the ground
    public bool jumping; // check is the player jumping 
    public float jumpTime;
    public float speed = 10f;
    public float jumpspeed = 20f;
    public float maxJumpTime = 0.2f;

    public float moveX; // get the input from the Controller in Move function

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Transform transform;

    //Set the View
    public void SetView(View theView)
    {
        View = theView;
    }

    // Set the Manager
    public void SetManager(Manager theManager)
    {
        Manager = theManager;
        rb = Manager.rb;
        boxCollider = Manager.boxCollider;
        transform = Manager.transform;
    }

    // Control character movement.
    public void Move(float moveX, bool jump)
    {
        // pass the input value from Controller to local variable
        if (grounded)
        {
            this.moveX = moveX;
            if (jumpTime < 0.0f)
            {
                jumping = false;
            }

        }

        // jump if the player is grounded and not currently jumping
        if (grounded && jump && !jumping)
        {
            jumping = true;
            jumpTime = maxJumpTime;
        }
        //reduce the jumpingtime 
        jumpTime -= Time.deltaTime;

        // Control the player to jump
        if (jumping)
        {
            if (jumpTime > 0.0f)
            {
                rb.velocity = new Vector2(moveX * speed, jumpspeed);
            }
        }
        // Control the player to run
        else if (grounded)
        {
            rb.velocity = new Vector2(moveX * speed, 0.0f);
        }

        // check does the player's sprite need to be flip, then pass that to the View script
        float smallMove = 0.0001f;
        if (moveX < -smallMove)
        {
            View.FlipX(true); 
        }
        else if (moveX > smallMove)
        {
            View.FlipX(false);   
        }

        // Call function in View to play the animation
        View.GroundAnimation(grounded);
        View.MoveAnimation(moveX);

    }


}
