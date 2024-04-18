using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float lifetime;

    public float lifetimeCounter;



    public float speed;
    // Update is called once per frame
    void Update()
    {
        lifetimeCounter += Time.deltaTime;

        transform.position += transform.forward * Time.deltaTime * speed;

        if (lifetimeCounter > lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Debug.Log("Good Shot");
            //enemyBehavior.health -= 25;
        }
    }
}