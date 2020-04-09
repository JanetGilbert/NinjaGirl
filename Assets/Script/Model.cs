using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data and logic.
public class Model
{
    BoxCollider2D boxCollider;

    // Game behaviour settings
    public float speed = 3.0f;
    public float jumpSpeed = 5f;
    public float jumpTime;
    public float maxJumpTime = 2f;

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
    public void Move(float moveX, bool jump)
    {
        grounded = CheckGrounded;


        if (grounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(moveX, 0.0f, 0.0f);
            moveDirection *= speed;

            // Reverses sprite orientation based on horizontal movement
            float smallMove = 0.0001f;
            if (moveX < -smallMove)
            {
                myView.SetFlip(true);
            }
            else if (moveX > smallMove)
            {
                myView.SetFlip(false);
            }
            myView.SetAnimationSpeed(Mathf.Abs(moveX));
            myView.SetGrounded(grounded);

            if (jump)
            {
                moveDirection.y = jumpSpeed;
            }


        }

        //moveDirection.y -= gravity * Time.deltaTime;

        //myController.Move(moveDirection * Time.deltaTime);
        myController.Move(moveDirection);
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
        boxCollider = myController.gameObject.GetComponent<BoxCollider2D>();
    }

    public bool CheckGrounded
    {
        get
        {
            grounded = false;

            //Returns false if not connected to a controller
            if (myController == null) return grounded;


            // Determines distance between ninja and ground
            float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
            float rayLen = 0.05f;
            Vector2 pos2D = (Vector2) myController.gameObject.transform.position;
            //Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

            //The "grounded" boolean defaults to false, assuming the ninja is off the ground
            grounded = false;


            //Determines whether or not the ninja is actually off the ground
            if (hit.collider != null)
            {
                Debug.Log("Hi" + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Platform"))
                {
                    //In this circumstance, the ninja is in fact not off the ground.
                    grounded = true;
                }
            }

            Debug.Log("EndOfFunction");
            return grounded;
        }
    }
}
