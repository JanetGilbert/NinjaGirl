using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
       
        

    }

    public void GroundAnimation(bool grounded)
    {
        animator.SetBool("grounded", grounded);
    }

    public void MoveAnimation(float moveX)
    {
        animator.SetFloat("speed", Mathf.Abs(moveX));
    }

    public void FlipX(bool flip)
    {
        sprite.flipX = flip;

    }
}
