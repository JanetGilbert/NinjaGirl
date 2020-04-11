using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //contain the access to the model since it's not MonoBehaviour
    public Model Model;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public Transform transform;
    public float speed = 10f;
    public float jumpspeed = 30f;
    public GameObject Ghost; // reference to the Ghost prefab

    void Awake()
    {
        //Get the Compoment from the player 
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform = GetComponent<Transform>();
        // create the Model and call the function in it to set the View and the Manager 
        Model = new Model();
        Model.SetView(GetComponent<View>());
        Model.SetManager(this);
        // Start Generate Ghost
        StartCoroutine(Generate());
    }

    void Update()
    {
        // Chekc is the player on ground each frame;
        GroundCheck();
    }

    // Check if the player on ground or not
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

    // Generate ghost every 3 sec
    private IEnumerator Generate()
    {
        while (true)
        {
            GameObject temp = Instantiate(Ghost, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);

        }
    }


}
