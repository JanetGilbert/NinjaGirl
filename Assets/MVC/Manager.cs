using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Model theModel;

    //Create model class and configure
    void Awake()
    {
        theModel = new Model();
        theModel.theView = GetComponent<View>();
        theModel.rb = GetComponent<Rigidbody2D>();
    }
}
