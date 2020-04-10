using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float moveX;
    bool jump;

    Model theModel;

    
    void Start()
    {
        theModel = GetComponent<Manager>().Model;
    }

    
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown(KeyCode.W);
        theModel.Move(moveX, jump); // Pass input to model.

    }

    
}
