﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_enemy : MonoBehaviour
{
    public int time_before_turn;
    public float timer;

    Rigidbody2D rigidbody;

    int signed = 1;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 0.02f;
        rigidbody.AddForce(Vector2.left * signed * speed);
        if (timer > time_before_turn)
        {
            
            signed *= -1;
            timer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Projectile")){
            SoundManager.instance.PlaySoundSideEnemy();
            Destroy(gameObject);
        }
    }
}
