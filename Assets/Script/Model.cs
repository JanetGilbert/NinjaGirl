using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data and logic.
public class Model
{
    // Game behaviour settings
    public const float speed = 1.0f;
    public const float jumpSpeed = 1.0f;
    public const float maxJumpTime = 1.0f;

    // Link to view.
    public View myView;

    //Link to controller
    public Controller myController;

    // Is the character grounded?
    private bool _isGrounded;

    public bool grounded
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

    private Vector3 moveDirection = Vector3.zero;

    // Control character movement.
    public void Move(float moveX, float moveY, bool jump)
    {
        grounded = myController.Grounded;


        if (grounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(moveX, 0.0f, moveY);
            moveDirection *= speed;

            // Face in direction of movement.
            if (moveDirection.magnitude > float.Epsilon)
            {
                myView.SetRotate(Quaternion.LookRotation(moveDirection));
            }

            myView.SetAnimationSpeed(moveDirection.magnitude);

            if (jump)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        //moveDirection.y -= gravity * Time.deltaTime;

        myController.Move(moveDirection * Time.deltaTime);
    }

    // Pass view into model.
    public void SetView(View theView)
    {
        myView = theView;
    }

    // Pass character controller into model.
    public void SetController(Controller theController)
    {
        myController = theController;
    }


}
