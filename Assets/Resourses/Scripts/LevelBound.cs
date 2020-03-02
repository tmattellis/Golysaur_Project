using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBound : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            print(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
