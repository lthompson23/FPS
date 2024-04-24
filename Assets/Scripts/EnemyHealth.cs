using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int eHealth = 100;

    public void Update()
    {
        if (eHealth <= 0) 
        {
            Destroy(this.gameObject);

            uiManager.enemyLeft -= 1; 

            if (playerHealth.health <= 90)
            {
                playerHealth.health += 10;
            }
        }


    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == ("Player Bullet"))
        {
            eHealth -= 50; 
        }
    }

    private void Start()
    {
        eHealth = 100;
    }
}
