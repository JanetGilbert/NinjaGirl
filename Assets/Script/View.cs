using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Display
public class View : MonoBehaviour
{
    public Animator animator; // Animation is specific to view.
    public SpriteRenderer sprite;

    public void SetFlip(bool isFlipped)
    {
        sprite.flipX = isFlipped;

    }

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


   

    public void SetGrounded(bool isGrounded)
    {
        animator.SetBool("grounded", isGrounded);
    }

    public void SetRotate(Quaternion rot)
    {
        transform.rotation = rot; // Is the rotation View or Model? Or both?
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }
}
