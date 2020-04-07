using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Since the model isn't a monobehaviour it needs a monobehaviour container to store and initialize it.
    public Model theModel;

    void Awake()
    {
        // Create model and supply view.
        theModel = new Model();
        theModel.SetView(GetComponent<View>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
