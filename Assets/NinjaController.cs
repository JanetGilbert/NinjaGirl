using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    Ninja ninjaModel;
    // Start is called before the first frame update
    void Start()
    {
        ninjaModel = GetComponent<Ninja>();
    }

    
    void Update()
    {
        UpdateMovementInput();
    }

    void UpdateMovementInput()
    {
        ninjaModel.moveX = Input.GetAxis("Horizontal");

        //If jump button, is pushed, execute jump function
        if (Input.GetButtonDown("Jump"))
        {
            ninjaModel.Jump();
        }
    }
}
