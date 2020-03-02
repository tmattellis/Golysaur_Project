using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D _rigidbody2D;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    SpriteRenderer sprite;
    Animator animator;



    public int jumpsLeft;



    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();


    }

   
    void Update()
    {
        

        // Move Left
        if(Input.GetKey(KeyCode.A)) {
            _rigidbody2D.AddForce(Vector2.left * 10f);
            sprite.flipX = true;
        }

        // Move Right
        if (Input.GetKey(KeyCode.D)) {
            _rigidbody2D.AddForce(Vector2.right * 10f);
            sprite.flipX = false;
        }

        // Aim toward Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);


        // Shoot
        if (Input.GetMouseButtonDown(0)) {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (jumpsLeft > 0) {
                jumpsLeft--;
                // Use a ForceMode2D of Impulse so that the full jump force is applied instantaneously
                _rigidbody2D.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
            }
        }
        animator.SetInteger("JumpsLeft", jumpsLeft);

        
    }

    void FixedUpdate() {
        animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
        if(_rigidbody2D.velocity.magnitude > 0) {
            // we're moving
            animator.speed = _rigidbody2D.velocity.magnitude / 3f;
        } else {
            animator.speed = 1f;
        }
    }

  
    void OnCollisionStay2D(Collision2D other) {
      
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
           
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 1.0f);
            Debug.DrawRay(transform.position, -transform.up * 1.0f);

            for (int i = 0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];
  
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                  
                    jumpsLeft = 2;
                }
            }
        }


    }
}
