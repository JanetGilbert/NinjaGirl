using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the View script of the MVC
public class View : MonoBehaviour
{
    
    Animator animator;
    SpriteRenderer sprite;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }


    //Play the landing and standing animation
    public void GroundAnimation(bool grounded)
    {
        animator.SetBool("grounded", grounded);
    }

    // Player the running animation
    public void MoveAnimation(float moveX)
    {
        animator.SetFloat("speed", Mathf.Abs(moveX));
    }

    //Flip the sprite when player change moving direction
    public void FlipX(bool flip)
    {
        sprite.flipX = flip;

    }

}
