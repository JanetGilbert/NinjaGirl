using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Kate Howell
 * Original Script By: Janet Gilbert
 * 
 * In the revised version of this project, This class will functin as the View in The MVC pattern
 * This class will store the visual controls for the Ninja Object
 */
public class View : MonoBehaviour
{
    private bool grounded;

    //Ninja Object Components
    Animator animator;
    SpriteRenderer sprite;

    void Awake()
    {
        //get instances of each necessary component
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    //Sets grounded var for animator
    public void SetGrounded(bool isGrounded)
    {
        grounded = isGrounded;
        animator.SetBool("grounded", grounded);
    }

    //Controls Ninja Sprite
    public void SetSprite(float moveX)
    {
        //Sprite Direction based off minimum movement
        float smallMove = 0.0001f;
        if (moveX < -smallMove)
        {
            sprite.flipX = true;
        }
        else if (moveX > smallMove)
        {
            sprite.flipX = false;
        }

        animator.SetFloat("speed", Mathf.Abs(moveX));
    }
}
