using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    

    public Transform player;

    public LayerMask whatIsGround;

    public LayerMask whatIsPlayer;

    public Vector3 walkPoint;

    bool walkPointSet;

    public float walkPointRange;

    public float timeBTW;

    bool hasAttacked;

    public float sightRange;

    public float attackRange;

    public bool inSightRange;

    public bool inAttackRange;

    public float lifetime;

    public float lifetimeCounter;

    public GameObject bulletPrefab;

    public float speed;

    public GameObject bulletPoint;


    private void Update()
    {
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inSightRange && !inAttackRange)
            Patrol();

        if (inSightRange && !inAttackRange)
            Chase();

        if (inSightRange && inAttackRange) 
            Attack();
    }

    private void findPoint()
    {
        float randomz = Random.Range(-walkPointRange, walkPointRange);

        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true; 
    }
    public void Patrol()
    {
        if (!walkPointSet)
            findPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToPoint = transform.position - walkPoint;

        if (distanceToPoint.magnitude < 1f)
            walkPointSet = false; 
    }
    public void Chase()
    {
        agent.SetDestination(player.position);
    }
    public void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!hasAttacked)
        {

            
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);

           
            Vector3 direction = Camera.main.transform.forward;

            
            bullet.GetComponent<Rigidbody>().AddForce(direction * speed);

            hasAttacked = true;
            Invoke(nameof(ResetAttack), timeBTW);
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
        Debug.Log("Enemy Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow; 

        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
