using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Kate Howell
 * Original Script By: Janet Gilbert
 * 
 * In the revised version of this project, This class will functin as the Controller in The MVC pattern
 * This class will store the Input control for the Ninja Object
 */
public class Controller : MonoBehaviour
{
    private float moveX; //X axis Input
    private bool jump; //Jump Input

    Ninja model;
    View modelView;

    void Awake()
    {
        model = GetComponent<Ninja>();
        modelView = GetComponent<View>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal"); // get X Axis input 
        jump = Input.GetButtonDown("Jump");//get jump input

        modelView.SetSprite(moveX); //Control sprite based on movement

    }

    //Fixed Update is used for in Game Physics
    private void FixedUpdate()
    {
        model.Move(moveX, jump);
    }
}
