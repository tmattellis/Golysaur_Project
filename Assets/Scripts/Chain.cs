using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    // Start is called before the first frame update
    bool canClimb = false;
    float speed = 5;

    void Start()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (GetComponent<SpriteRenderer>().enabled == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                    //other.GetComponent<Rigidbody2D>().gravityScale = 0;

                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                    //other.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
                else
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }

        }
    }
}
