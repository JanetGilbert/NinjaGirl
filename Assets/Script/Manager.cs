using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //contain the access to the model since it's not MonoBehaviour
    public Model Model;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public float speed = 10f;//
    public float jumpspeed = 20f;//

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        Model = new Model();
        Model.SetView(GetComponent<View>());
        Model.SetManager(this);
    }

    void Update()
    {
        GroundCheck();
    }

    public void GroundCheck()
    {
        float selfHeightOffset = (boxCollider.size.y / 2.0f) + 0.1f;
        float rayLen = 0.05f;
        Vector2 pos2D = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos2D - (Vector2.up * selfHeightOffset), -Vector2.up, rayLen);

        Model.grounded = false;
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                Model.grounded = true;
            }
        }

    }


}
