using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Display
public class View: MonoBehaviour   
    {   
    public Animator animator; // Animation is specific to view.
    SpriteRenderer sprite;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        float smallMove = 0.0001f;
        if (Controller.xMove < -smallMove)
        {
            sprite.flipX = true;
        }
        else if (Controller.xMove> smallMove)
        {
            sprite.flipX = false;
        }

    }

    public void SetGrounded(bool isGrounded)
    {
        animator.SetBool("grounded", isGrounded);
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.SetFloat("speed", Mathf.Abs(speed));
    }
}
