using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{

    public float speed = 1.0f;
    public float jumpspeed = 1.0f;
    public float maxJumpTime = 1.0f;
    private float jumpTime;

    bool jumping;

    public View myView;

    private CharacterController myCharacterController;

    private bool _grounded;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer sprite;

    public bool grounded
    {
        get { return _grounded; }

        set
        {
            _grounded = value;

            myView.SetGrounded(_grounded); // Update the view.
        }
    }

    private Vector3 moveDirection = Vector3.zero;

    public void Move(float moveX, bool jumppressed)
    {

        grounded = myCharacterController.isGrounded;
         
         float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
         float rayLen =  0.05f;
         Vector2 pos2D = (Vector2)transform.position;
         // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
         RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);
 
         grounded = false;
         if (hit.collider != null)
         {
             if (hit.collider.CompareTag("Platform"))
             {
                 grounded = true;
             }
         }
 
         // Jumping
         if (grounded)
         {
             moveDirection = new Vector3(moveX, 0f, 0f);
 
 
             if (jumpTime < 0.0f)
             {
                 jumping = false;
             }
         }
 
 
         bool jumpPressed = Input.GetButtonDown("Jump");
         if (grounded && jumpPressed && !jumping)
         {
             jumping = true;
             jumpTime = maxJumpTime;
         }
         
         float smallMove = 0.0001f;
         if (moveX < -smallMove)
         {
             sprite.flipX = true;
         }
         else if (moveX > smallMove)
         {
             sprite.flipX = false;
         }
 
 
     }

     public void SetView(View theView)
     {
         myView = theView;
     }

     // Pass character controller into model.
     public void SetCharacterController(CharacterController theCharacterController)
     {
         myCharacterController = theCharacterController;
     }

     
}
