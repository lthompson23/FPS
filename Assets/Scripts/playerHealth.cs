using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{

    public static int health;

    

    private void Start()
    {
        health = 100;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy Bullet")
        {
            health -= 5;
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
    }
}
