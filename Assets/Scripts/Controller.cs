using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kevin Garcia
//4/17/20
//This script follows the MVC pattern and stores the input control for the model


// Control system
public class Controller : MonoBehaviour
{
    private float moveX;
    private float moveY;
    bool jump;

    Model theModel;
    View theView;

    void Awake()
    {
        theModel = GetComponent<Manager>().theModel;
        theModel.SetCharacterController(GetComponent<CharacterController>());

        theView = GetComponent<View>();
    }

    void Update()
    {
        //Get the Input
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");
    }

    //Physics always should be in FixedUpdate
    void FixedUpdate()
    {
        theModel.Move(xMove, yMove, jump); // Pass input to model.
    }
}
