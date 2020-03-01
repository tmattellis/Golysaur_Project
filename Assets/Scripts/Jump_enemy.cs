using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;


    public float jump_height;
    public float time_between_jump;
    public float jump_timer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        jump_timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        jump_timer += Time.deltaTime;
        if(jump_timer > time_between_jump){
            rigidbody.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
            jump_timer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Projectile")){
            Destroy(gameObject);
        }
    }
}