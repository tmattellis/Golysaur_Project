using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
   
    public GameObject[] targets;

    void Start()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<SpriteRenderer>().enabled = false;
          
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            // loop through the array and set them active/inactive
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
