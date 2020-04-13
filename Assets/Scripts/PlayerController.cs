using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    AudioSource audiosrc;
    public int jumpsLeft;

    public double highJumpTimer;
    public double speedTimer;
    public int projectileNum;
    public int startProjectileNum;
    public int projectileIncrement;
    public int Health;

    //ability modifiers
    public float speedAbility;
    public float jumpAbility;
    public float projectileAbility;
    public float powerUpAbility;

    public static PlayerController instance;

    // outlets
    public GameObject projectilePrefab;
    public GameObject fireballPrefab;
    Animator animator;
    
    // set to false by default
    public bool antiGravAvailable = false;

    SpriteRenderer playerSprite;
    private int gravScale = 1;
    public bool isPaused;

    // for ladder
    bool canClimb = false;
    float speed = 1;
    private float inputVertical;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audiosrc = GetComponent<AudioSource>();
        projectileNum = startProjectileNum;

        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    
    // for animations
    void FixedUpdate()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            MenuController.instance.Show();
        }
        
        //framerate
        if (Application.targetFrameRate > 60)
        {
            Application.targetFrameRate = 60;
        }
            

        // move player left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (speedTimer > 0)
            {
                rigidbody.AddForce(Vector2.left * (20f + speedAbility) * Time.deltaTime * 60f);
            }
            else
            {
                rigidbody.AddForce(Vector2.left * (10f + speedAbility) * Time.deltaTime * 60f);
            }
        }

        // move player right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (speedTimer > 0)
            {
                rigidbody.AddForce(Vector2.right * (20f + speedAbility) * Time.deltaTime * 60f);
            }
            else
            {
                rigidbody.AddForce(Vector2.right * (10f + speedAbility) * Time.deltaTime * 60f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, 1.1f);


                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];

                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        rigidbody.AddForce((Vector2.left*0.7f + Vector2.up) * (7f + jumpAbility) * gravScale, ForceMode2D.Impulse);
                        audiosrc.Play();

                    }
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
               
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.right, 1.1f); 


                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];

                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        rigidbody.AddForce((Vector2.right*0.7f + Vector2.up) * (7f + jumpAbility) * gravScale, ForceMode2D.Impulse);
                        audiosrc.Play();

                    }
                }
            }
            // else
            // {
                if (jumpsLeft > 0)
                {
                    jumpsLeft--;

                    if (highJumpTimer > 0)
                    {
                        rigidbody.AddForce(Vector2.up * (14f + jumpAbility) * gravScale, ForceMode2D.Impulse);
                    }
                    else
                    {
                        rigidbody.AddForce(Vector2.up * (6f + jumpAbility) * gravScale, ForceMode2D.Impulse);
                        audiosrc.Play();
                    }
                }
            //}


        }
        
        if (Input.GetKeyDown(KeyCode.G) && antiGravAvailable)
        {
            rigidbody.gravityScale *= -1;
            gravScale *= -1;
            rigidbody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            playerSprite.flipY = !playerSprite.flipY;
            //transform.rotation = Quaternion.AngleAxis(180, Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (projectileNum > 0)
            {
                SoundManager.instance.PlaySoundArrow();
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = transform.rotation;
                newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * (5f + projectileAbility);
                projectileNum--;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (projectileNum > 0)
            {
                SoundManager.instance.PlaySoundArrow();
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = transform.rotation;
                newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * -(5f+projectileAbility);
                newProjectile.GetComponent<SpriteRenderer>().flipX = true;
                projectileNum--;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (projectileNum > 0)
            {
                SoundManager.instance.PlaySoundFireball();
                GameObject newProjectile = Instantiate(fireballPrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = transform.rotation;
                newProjectile.GetComponent<Rigidbody2D>().velocity = transform.up * (2f+projectileAbility);
                projectileNum--;
            }
        }

        // use delta time for frame rate independence
        if (highJumpTimer > 0)
        {
            highJumpTimer = highJumpTimer - Time.deltaTime;
        }
        if (speedTimer > 0)
        {
            speedTimer = speedTimer - Time.deltaTime;
        }
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        // check that we collided with ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // multiply by gravScale to account for reversing gravity
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up * gravScale, 1.1f);
            //Debug.DrawRay(transform.position, -transform.up *1f); // Vizualize RayCast

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 1;
                }
            }
        }


    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<HighJumpGem>())
        {
            highJumpTimer = 10f + powerUpAbility*2f;
        }

        if (other.gameObject.GetComponent<SpeedGem>())
        {
            speedTimer = 10f + powerUpAbility*2f;
        }

        if (other.gameObject.GetComponent<ProjectileStar>())
        {
            projectileNum += 5 + (int) projectileAbility;
        }

		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
		//spike

		if (other.gameObject.GetComponent<Spike>())
        {
            TakeDamage(10);
            Debug.Log("Take damage 10");

        }
       
    }
   

    
    
    
    void TakeDamage (int dmg)
	{
        Health -= dmg; 
 	}
    

}
