using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GroundDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PlaySoundGround();
            Destroy(gameObject, 1);
        }
    }
}
