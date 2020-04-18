using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Kevin Garcia
//4/17/2020
//This script stores the model since it isnt a monobehavior

public class NinjaManager : MonoBehaviour
{
    //Variables:
    public Model theModel;
    public Rigidbody2D rb2D;
    public BoxCollider2D boxCollider2D;
    public Transform transform;
    public float speed = 10f;
    public float jumpspeed = 30f;

    void Awake()
    {
        //Store the components in their variable from the attached game object
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        transform = GetComponent<Transform>();

        // Create model and set the view.
        theModel = GetComponent<Model>();
        theModel.SetView(GetComponent<View>());
    }

}
