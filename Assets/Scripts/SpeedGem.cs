﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGem : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
