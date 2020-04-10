using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model 
{
    //access to the View
    public View View;
    public Manager Manager;

    public bool grounded;
    public bool jumping;
    public float jumpTime;
    public float speed = 10f;
    public float jumpspeed = 20f;
    public float maxJumpTime = 0.2f;

    public float moveX;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
  
 
    public void SetView(View theView)
    {
        View = theView;
    }

    public void SetManager(Manager theManager)
    {
        Manager = theManager;
        rb = Manager.rb;
        boxCollider = Manager.boxCollider;
    }

    // Control character movement.
    public void Move(float moveX, bool jump)
    {
        if (grounded)
        {
            this.moveX = moveX;
            if (jumpTime < 0.0f)
            {
                jumping = false;
            }

        }

        if (grounded && jump && !jumping)
        {
            jumping = true;
            jumpTime = maxJumpTime;
        }
        jumpTime -= Time.deltaTime;


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

        float smallMove = 0.0001f;
        if (moveX < -smallMove)
        {
            View.FlipX(true); 
        }
        else if (moveX > smallMove)
        {
            View.FlipX(false);   
        }

        View.GroundAnimation(grounded);
        View.MoveAnimation(moveX);

    }


}
