using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public int jumpsLeft;

    public double highJumpTimer;
    public double speedTimer;
    public int projectileNum;
    public int startProjectileNum;
    public int projectileIncrement;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        projectileNum = startProjectileNum;
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
                    rigidbody.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                }
            }
        }


        if(Input.GetKeyDown(KeyCode.D))
        {
            if(projectileNum > 0)
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = transform.rotation;
                newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
                projectileNum --;
            }
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            if(projectileNum > 0)
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = transform.rotation;
                newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * -10f;
                newProjectile.GetComponent<SpriteRenderer>().flipX = true;
                projectileNum --;
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
            projectileNum += projectileIncrement;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Enemies")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // if(other.gameObject.GetComponent<Door>())
        // {
        //     transform.position = new Vector3(-8.2f,-1f,0f);
        // }
    }

    private GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 35;
        GUI.Label(new Rect(10, 800, 100, 20), "Arrows Left: " + projectileNum.ToString(), guiStyle);
    }
}
