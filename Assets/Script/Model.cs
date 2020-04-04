using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Data and logic.
public class Model
{
    public const float speed = 10.0f;
    public const float jumpSpeed = 5.0f;
    public const float gravity = 7.0f;
  
    private bool _isGrounded;

    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }

        set
        {
            _isGrounded = value;

            myView.SetGrounded(_isGrounded); // Update the view.
        }
    }



    public View myView;
   
    public float moveX;
    
    private CharacterController thisCharacterController;

    private Vector2 moveDirection = Vector2.zero;
    public void Move(float moveX, bool jump)
    {

        isGrounded = thisCharacterController.isGrounded;

        if (isGrounded)
        {
            // We are grounded
            // move direction directly from axes

            moveDirection = new Vector3(moveX, 0.0f);
            moveDirection *= speed;


            myView.SetAnimationSpeed(moveX);

            if (jump)
            {
                moveDirection.y = jumpSpeed;

            }

        }
        else
        {
            moveDirection = new Vector3(moveX, moveDirection.y);
            moveDirection.x *= speed/2;
            myView.SetAnimationSpeed(moveX/2);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        thisCharacterController.Move(moveDirection * Time.deltaTime);
    }


    public float maxJumpTime = 1.0f;


    // Pass view into model.
    public void SetView(View theView)
    {
        myView = theView;
    }
    // Pass rigidbody into model.
    
    public void SetCharacterController(CharacterController characterController)
    {
        thisCharacterController = characterController;
    }


}
