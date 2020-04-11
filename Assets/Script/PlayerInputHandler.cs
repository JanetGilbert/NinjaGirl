using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerModel model;                      // The model
    [SerializeField]
    private float runAcceloration, topRunSpeed;     // The acceloration and top speed of the player
    [SerializeField]
    [Range(0,1f)]
    private float runDeadening;                     // This value is used to decrease the running speed while not pushing any inputs
    [SerializeField]
    float groundCheckOffset, groundCheckDistance;   // The offset and length of the raycast to check if the player is grounded
    bool jumpFlag;
    [SerializeField]
    public float jumpForce;                         // Force used to make the player jump

    private void Awake()
    {
        // Instantiate the model and pass it the required components
        model = new PlayerModel(GetComponent<Rigidbody2D>(), GetComponent<BoxCollider2D>(), GetComponent<Animator>(), GetComponent<SpriteRenderer>());
    }

    void Start()
    {
        
    }

    void Update()
    {
        // We read the input here because we don't want to miss the call but we want to run the jump code in fixed update so set a flag
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

        // Exacute any waiting jump commands
        if(jumpFlag)
        {
            model.Jump(jumpForce);
            jumpFlag = false;
        }
    }
}
