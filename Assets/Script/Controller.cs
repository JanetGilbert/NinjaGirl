using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control system
public class Controller : MonoBehaviour
{
    private float moveX;
    bool jumping;

    Model theModel;
    Rigidbody2D rb;


    void Start()
    {
        theModel = GetComponent<Manager>().theModel;
        theModel.SetController(this);
        rb = gameObject.GetComponent<Rigidbody2D>();
        theModel.Move(0, false);
    }

    void Update()
    {
        if (theModel == null) return;

        //Ninja cannot jump or change direction while not on the ground
        //(Ninja is not a very good ninja by cartoon ninja standards)
        if (theModel.grounded)
        {
            //Reads horizontal control input
            moveX = Input.GetAxis("Horizontal");

            //Determines whether the ninja is still jumping
            if (theModel.jumpTime < 0.0f)
            {
                jumping = false;
            }
        }

        //Determines when the ninja jumps
        if (theModel.grounded && Input.GetButtonDown("Jump") && !jumping)
        {
            jumping = true;
            theModel.jumpTime = theModel.maxJumpTime;
        }

        //Counts how long until the ninja can jump again
        theModel.jumpTime -= Time.deltaTime;
    }

    // Actual movement
    private void FixedUpdate()
    {
        if (theModel == null) return;

        bool check = theModel.CheckGrounded;

        //Sets initial horizontal and vertical velocity
        //(Ninja jumps off ground and maintains horizontal velocity while gravity reduces vertical velocity) 
        if (jumping)
        {
            if (theModel.jumpTime > 0.0f)
            {
                theModel.Move(moveX * theModel.speed, true);
            }
        }

        //Maintains horizontal velocity
        else if (theModel.grounded)
        {
            theModel.Move(moveX * theModel.speed, false);
        }
    }





    public void Move(Vector3 direction)
    {
        if (rb == null) return;
        Debug.Log(direction);
        rb.velocity = direction;
    }
}
