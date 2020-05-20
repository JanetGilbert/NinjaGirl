using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaView : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    public void UpdateAnimator(float moveX, bool grounded)
    {

        //Based on the direction of MoveX, Flip the Sprite
        //If the move is so tiny (e.g.) small move, then ignore flipping
        float smallMove = 0.0001f;
        if (moveX < -smallMove)
        {
            sprite.flipX = true;
        }
        else if (moveX > smallMove)
        {
            sprite.flipX = false;
        }

        // Animator parameters
        //Sets the animation paramaters /position of the character based on its speed and groundedness
        animator.SetFloat("speed", Mathf.Abs(moveX));
        animator.SetBool("grounded", grounded);
    }
}
