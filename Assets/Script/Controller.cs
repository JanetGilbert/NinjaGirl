using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    float moveX;
    bool jumppressed;

    
    Model theModel;

    float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        theModel = GetComponent<Manager>().theModel;
        theModel.SetCharacterController(GetComponent<CharacterController>());
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        moveX = Input.GetAxis("Horizontal");
        jumppressed = Input.GetButton("Jump");
    }

    // Physics always should be in FixedUpdate
    void FixedUpdate()
    {
        theModel.Move(moveX, jumppressed); // Pass input to model.
    }
}
