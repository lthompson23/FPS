using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class playerShoot : MonoBehaviour
{
    public float lifetime;

    public AudioSource shoot; 

    public float lifetimeCounter;

    public float fireCooldown;

    public float fireRate;

    public GameObject bulletPrefab;

    public float speed;

    public GameObject bulletPoint;



    void Start()
    {
        fireCooldown = fireRate;
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot.Play();
            // Instantiate the bullet at bulletPoint's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);

            // Calculate the direction the player is facing
            Vector3 direction = Camera.main.transform.forward;

            // Apply force to the bullet in the direction the player is facing
            bullet.GetComponent<Rigidbody>().AddForce(direction * speed);
        }
    }
    void Update()
    {
        fireCooldown += Time.deltaTime;

        if (fireCooldown >= fireRate && Input.GetMouseButtonDown(0))
        {
            //Call the Shoot function 
            Shoot();

            //Reset the cooldown
            fireCooldown = 0;


        }
    }
}