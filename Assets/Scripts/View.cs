using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Kevin Garcia
//4/17/20
//This script is revised in the MVC pattern and stores 
//the visual controls for the model object

public class View : MonoBehaviour
{
    //Object Component references:
    public Animator animator; // Animation is specific to view.
    public SpiteRenderer spiteRenderer;

    public bool grounded;

    void Awake()
    {
        //Get references to the variables and store them
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    //Sets the grounded variable to be referenced by the animator
    public void SetGrounded(bool isGrounded)
    {
        grounded = isGrounded;
        animator.SetBool("Grounded", isGrounded);
    }

    //Set the rotation
    public void SetRotate(Quaternion rot)
    {
        transform.rotation = rot; 
    }

    //Set the animation speed
    public void SetAnimationSpeed(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }


}
