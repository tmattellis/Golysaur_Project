using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_enemy : MonoBehaviour
{

    Rigidbody2D rigidbody;

    SpriteRenderer m_SpriteRenderer;


    Transform character;

    public float range;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        character = GameObject.Find("Character").transform;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec2player = character.position - transform.position;
        vec2player = vec2player / vec2player.magnitude;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, vec2player, range);
        //Debug.DrawRay(transform.position, vec2player* range);
        for (int i = 0; i<hits.Length;i++)
            {
                RaycastHit2D hit = hits[i];
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    rigidbody.AddForce(vec2player*speed);
                }
            }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Projectile")){
            SoundManager.instance.PlaySoundFly();
            m_SpriteRenderer.color = Color.red;
            Destroy(gameObject, 1.5f);
        }
    }
}
