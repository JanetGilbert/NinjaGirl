using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control system
public class Controller : MonoBehaviour
{
    private float xMove;
    private float yMove;
    bool jump;

    Model theModel;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;


    void Start()
    {
        theModel = GetComponent<Manager>().theModel;
        theModel.SetController(this);
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");
    }

    // Physics always should be in FixedUpdate
    void FixedUpdate()
    {
        theModel.Move(xMove, yMove, jump); // Pass input to model.
    }



    public bool Grounded
    {
        get
        {
            bool grounded;
            // Determines distance between ninja and ground
            float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
            float rayLen = 0.05f;
            Vector2 pos2D = (Vector2)transform.position;
            // Debug.DrawRay(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up * rayLen, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

            //The "grounded" boolean defaults to false, assuming the ninja is off the ground
            grounded = false;

            //Determines whether or not the ninja is actually off the ground
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    //In this circumstance, the ninja is in fact not off the ground.
                    grounded = true;
                }
            }

            return grounded;
        }
    }

    public void Move(Vector3 direction)
    {
        rb.velocity = direction;
    }
}
