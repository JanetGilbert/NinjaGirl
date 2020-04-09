using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    // Object references
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator animator;
    private SpriteRenderer sprite;

    private float runAcc;
    private bool grounded;

    public PlayerModel(Rigidbody2D rigidbody2D, BoxCollider2D boxCollider2D, Animator animator, SpriteRenderer spriteRenderer)
    {
        rb = rigidbody2D;
        collider = boxCollider2D;
        this.animator = animator;
        runAcc = 0f;
        sprite = spriteRenderer;
    }

    /// <summary>
    /// The function to make the player run.
    /// </summary>
    /// <param name="value">The base for the run direction calculations. -1 indicates moving left, 1 indicates moving right, and 0 indicates no input.</param>
    /// <param name="runAcceloration">The rate at which the player accelorates while holding an input.</param>
    /// <param name="topRunSpeed">The maximum speed the player can run at.</param>
    /// <param name="deadeningMultiplier">The value the acceloration is multiplied to deaden movement. Must be greater than 0 and less than 1.</param>
    public void Running(float value, float runAcceloration, float topRunSpeed, float deadeningMultiplier)
    {
        // Make sure the deadening multiplier is in the acceptible range.
        if(deadeningMultiplier < 0f || deadeningMultiplier >= 1f)
        {
            Debug.LogWarning("Recieved deadening multiplier out of acceptible range. Value was " + deadeningMultiplier + ", but must be greater than 0 and less than 1.");
            return;
        }
        
        // If are running left or right we have to increase our acceloration
        if (value > float.Epsilon || value < -float.Epsilon)
        {
            runAcc += value * runAcceloration * Time.deltaTime;
            runAcc = runAcc > 1f ? 1f : runAcc;
            runAcc = runAcc < -1f ? -1f : runAcc;
        }
        // Otherwise we deaden our movement
        else
        {
            runAcc *= deadeningMultiplier;
        }

        float vel = runAcc * topRunSpeed;

        animator.SetFloat("speed", Mathf.Abs(vel));

        if(vel >float.Epsilon)
        {
            sprite.flipX = false;
        }
        else if(vel < -float.Epsilon)
        {
            sprite.flipX = true;
        }

        // Change the rigidbody's velocity
        rb.velocity = new Vector2(vel, rb.velocity.y);
    }

    /// <summary>
    /// Checks if the player is currently on the ground.
    /// </summary>
    /// <param name="distanceFromBody">The distance from the collider to begin the raycast to check for ground.</param>
    /// <param name="raycastDistance">The length of the raycast to check for ground.</param>
    public void GroundCheck(float distanceFromBody, float raycastDistance)
    {
        Vector2 rayOffset = new Vector2(0, -1 * collider.size.y / 2f - distanceFromBody);

        RaycastHit2D hit = Physics2D.Raycast(rb.position + rayOffset, Vector2.down, raycastDistance);

        grounded = false;
        if(hit.collider != null)
        {
            grounded = hit.collider.tag == "Platform";
        }

        animator.SetBool("grounded", grounded);
    }

    /// <summary>
    /// Causes the player to jump.
    /// </summary>
    /// <param name="force">The force that is applied to the player's RigidBody2D</param>
    public void Jump(float force)
    {
        // You can't jump if you're not grounded
        if(!grounded)
            return;

        // Add the jump force
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}