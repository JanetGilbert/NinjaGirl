using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaManager : MonoBehaviour
{

    public Model theModel;
    
    // Start is called before the first frame update
    void Awake()
    {
        theModel = new Model();
        theModel.SetView(GetComponent<View>());
        
    }

    
}
