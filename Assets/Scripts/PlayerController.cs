using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public int jumpsLeft;

    public double highJumpTimer;
    public double speedTimer;
    public double projectileTimer;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move player left
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if(speedTimer > 0)
            {
                rigidbody.AddForce(Vector2.left *25f);
            }
            else
            {
                rigidbody.AddForce(Vector2.left *14f);
            }
        }

        // move player right
        if(Input.GetKey(KeyCode.RightArrow))
        {
            if(speedTimer > 0)
            {
                rigidbody.AddForce(Vector2.right *25f);
            }
            else
            {
                rigidbody.AddForce(Vector2.right *14f);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpsLeft > 0)
            {
                jumpsLeft--;

                if(highJumpTimer > 0)
                {
                    rigidbody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                }
            }
        }


        if(Input.GetKeyDown(KeyCode.A))
        {
            if(projectileTimer > 0)
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = transform.rotation;
            }
        }

        // use delta time for frame rate independence
        if(highJumpTimer > 0)
        {
            highJumpTimer = highJumpTimer - Time.deltaTime;
        }
        if(speedTimer > 0)
        {
            speedTimer = speedTimer - Time.deltaTime;
        }
        if(projectileTimer > 0)
        {
            projectileTimer = projectileTimer - Time.deltaTime;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        // check that we collided with ground
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.7f);
            // Debug.DrawRay(transform.position, -transform.up *0.7f); // Vizualize RayCast
            
            for(int i=0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 1;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<HighJumpGem>())
        {
            highJumpTimer = 10f;
        }

        if(other.gameObject.GetComponent<SpeedGem>())
        {
            speedTimer = 10f;
        }

        if(other.gameObject.GetComponent<ProjectileStar>())
        {
            projectileTimer = 15f;
        }
    }
}
