using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    private float _jumpTime = 0;
    private bool _jumping;

    private float _speed = 10;
    private float _jumpSpeed = 20;
    private float _maxJumpTime = .2f;

    public View theView;
    public Rigidbody2D rb;

    //Update Player movement and physics
    public void UpdateMovement(float xMove, bool jumpButton, bool grounded) {

        //Determine if Player can jump
        if (_jumpTime < 0.0f && grounded) _jumping = false;

        //Determine if the player is jumping, and for how long
        if (grounded && jumpButton && !_jumping) {
            _jumping = true;
            _jumpTime = _maxJumpTime;
        }

        _jumpTime -= Time.deltaTime;

        //Apply forces
        if (_jumping) {
            if (_jumpTime > 0.0f) {
                rb.velocity = new Vector2(xMove * _speed, _jumpSpeed);
            }
        } else if (grounded) {
            rb.velocity = new Vector2(xMove * _speed, 0.0f);
        }

        //Update View
        theView.determineSprite(xMove, grounded);
    }
}


