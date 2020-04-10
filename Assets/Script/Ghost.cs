using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    int dir; // the moving direction
    SpriteRenderer sprite;

    void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        dir = Random.Range(0, 2);// randomly assign the direction
        if (dir == 0) // set the start position and flip the sprite according to the direction
        {
            this.transform.position = new Vector3(36, -0.5f, 0);
        }
        else
        {
            sprite.flipX = true;
            this.transform.position = new Vector3(-9, -0.5f, 0);
        }
    }
  
    void Update()
    {
        // moving from right to left
        if (dir == 0)
        {
            this.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
        }
        // moving from left to right
        else
        {
            this.transform.position += new Vector3(3f * Time.deltaTime, 0, 0);
        }

        // Destroy its self when moving out of bounds
        if (transform.position.x > 50 || transform.position.x < -15)
        {
            Destroy(this.gameObject);
        }
    }

    //Change color when colliding with things
    private void OnTriggerEnter2D(Collider2D col)
    {
        sprite.color = new Vector4(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
      
    }
}
