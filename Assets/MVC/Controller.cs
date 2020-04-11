using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float _moveX;
    bool _jumpPressed;

    private Model theModel;

    void Start() {
        theModel = GetComponent<Manager>().theModel;    
    }

    void Update()
    {
        //Update Player Input for Movement and Jump
        _moveX = Input.GetAxis("Horizontal");
        _jumpPressed = Input.GetButton("Jump");

    }

    void FixedUpdate() {
        //Send Model Input Information
        theModel.UpdateMovement(_moveX, _jumpPressed, _isGrounded);
    }

    //Determine if Player is Currently Grounded
    bool _isGrounded {

        get {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.down, 3.15f);

            if (hit.collider != null) {
                if (hit.collider.CompareTag("Platform")) {
                    return true;
                }
            }
            return false;
        }
    }
}
