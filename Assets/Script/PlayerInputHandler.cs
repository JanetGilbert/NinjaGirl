using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerModel model;
    [SerializeField]
    private float runAcceloration, topRunSpeed;
    [SerializeField]
    [Range(0,1f)]
    private float runDeadening;
    [SerializeField]
    float groundCheckOffset, groundCheckDistance;
    bool jumpFlag;
    [SerializeField]
    public float jumpForce;

    private void Awake()
    {
        model = new PlayerModel(GetComponent<Rigidbody2D>(), GetComponent<BoxCollider2D>(), GetComponent<Animator>(), GetComponent<SpriteRenderer>());
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jumpFlag = true;
        }
    }

    void FixedUpdate()
    {
        // Running inputs
        float runDirection = 0f;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            runDirection = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            runDirection = -1f;
        model.Running(runDirection, runAcceloration, topRunSpeed, runDeadening);

        // Check if the player is grounded
        model.GroundCheck(groundCheckOffset, groundCheckDistance);

        if(jumpFlag)
        {
            model.Jump(jumpForce);
            jumpFlag = false;
        }
    }
}
