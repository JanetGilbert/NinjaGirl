using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Kevin Garcia
//4/17/2020
//This script is revised in the MVC pattern and stores 
//the data and logic for the model object.

public class Model
{
    //Get the view and store it in the variable:
    public View view;

    //Get the NinjaManager and store it in the variable:
    public NinjaManager ninjaManager;

    //Variables:
    public bool grounded; // check is the player on the ground
    public bool jumping; // check is the player jumping 
    public float jumpTime;
    public float speed = 10f;
    public float jumpspeed = 20f;
    public float maxJumpTime = 0.2f;
    public float moveX; // get the input from the Controller in Move function
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider2D;
    Transform transform;

    //Set the view with a function:
    public void SetView(view theView)
    {
        view = theView;
    }

    //Set the NinjaManager with a function:
    public void SetManager(ninjaManager theManager)
    {
        ninjaManager = theManager;
        rb2D = ninjaManager.rb;
        boxCollider2D = ninjaManager.boxCollider2D;
    }

    //Movement
    public void Movement(float moveX, bool jumping)
    {
        //Ground Check:
        float rayDist = 0.05f;
        Vector2 position = (Vector2)transform.position;
        RaycastHit2D rayHit = Physics2D.Raycast(position - Vector2.down, Vector2.down, rayDist);

        Model.grounded = false;
        if(rayHit.collider != null)
        {
            if (rayHit.collider.CompareTag("Ground"))
            {
                Model.grounded = true;
            }
        }
    }
}