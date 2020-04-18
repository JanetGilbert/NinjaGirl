using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static float xMove;
    
    bool jump;

    Model theModel;
    

    // Start is called before the first frame update
    void Start()
    {
        theModel = GetComponent<NinjaManager>().theModel;
        theModel.SetCharacterController(GetComponent<CharacterController>());
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
    }

   void FixedUpdate()
    {
        theModel.Move(xMove, jump); // Pass input to model.
    }

    

}
