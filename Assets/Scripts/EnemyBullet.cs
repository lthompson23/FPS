using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifetime;

    public float lifetimeCounter;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == ("Untagged"))
        {
            Destroy(this.gameObject);
        }
    }

    public float speed;
    // Update is called once per frame
    void Update()
    {
        lifetimeCounter += Time.deltaTime;

        transform.position += -transform.forward * Time.deltaTime * speed;

        if (lifetimeCounter > lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    
}
