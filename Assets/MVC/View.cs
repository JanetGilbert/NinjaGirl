using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;

    void Start() {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void determineSprite(float xMove, bool grounded) {

        // Reverse horizontal 
        float smallMove = 0.0001f;
        if (xMove < -smallMove) {
            sprite.flipX = true;
        } else if (xMove > smallMove) {
            sprite.flipX = false;
        }

        // Animator parameters
        if (!grounded) xMove = 0;
        animator.SetFloat("speed", Mathf.Abs(xMove));
        animator.SetBool("grounded", grounded);
    }
}
