using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Author: Kate Howell
 * Since the Ninja Model is not a monoBehavior, this class will store it
 * 
 */
public class Manager : MonoBehaviour
{

    public Ninja theNinja;

    void Awake()
    {
        //create ninja and set View component
        theNinja = GetComponent<Ninja>();
        theNinja.SetView(GetComponent<View>());
        theNinja.SetInput(GetComponent<Controller>());

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
