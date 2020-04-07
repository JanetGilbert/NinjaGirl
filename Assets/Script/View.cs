using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Display
public class View : MonoBehaviour
{
    public Animator animator; // Animation is specific to view.

    void Start()
    {

    }


    void Update()
    {

    }

    public void SetGrounded(bool isGrounded)
    {
        animator.SetBool("Grounded", isGrounded);
    }

    public void SetRotate(Quaternion rot)
    {
        transform.rotation = rot; // Is the rotation View or Model? Or both?
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }


}
