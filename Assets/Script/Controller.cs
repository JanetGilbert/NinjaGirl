using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the Controller script of the MVC
public class Controller : MonoBehaviour
{
    float moveX;
    bool jump;
    // access to the Model
    Model theModel;

    
    void Start()
    {
        theModel = GetComponent<Manager>().Model;
    }

    
    void Update()
    {
        // Take player's input
        moveX = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown(KeyCode.W);
        theModel.Move(moveX, jump); // Pass input to model.

    }

    
}
