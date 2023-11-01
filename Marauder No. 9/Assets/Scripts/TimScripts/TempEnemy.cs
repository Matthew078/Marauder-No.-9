using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { Idle, Patrol, Attack, Stunned };
public class TempEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GunScript gun;
    [SerializeField] private TempLootSplash lootSplash;
    [SerializeField] private Transform player;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float range = 8;
    [SerializeField] private float idleDelay = 3.5f;
    [SerializeField] private float idleTimer;
    [SerializeField] private float stunDelay = 1f;
    [SerializeField] private float stunTimer;
    [SerializeField] private float shootDelay = 5f;
    [SerializeField] private float shootTimer;
    public float health = 100;
    [SerializeField] private State currentState;

    bool facingForwards;
    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Patrol;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        lootSplash = GetComponent<TempLootSplash>();
        gun.gameObject.tag = "EnemyWeapon";
        idleTimer = 0f;
        stunTimer = 0f;
        shootTimer = 0f;

        currentTarget = pointA;
        agent.destination = currentTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.x > 0)
        {
            facingForwards = true;
        }
        else if (agent.velocity.x < 0)
        {
            facingForwards = false;
        }

        //Shoot Cooldown
        if (shootTimer < shootDelay)
        {
            shootTimer += Time.deltaTime;
        }
        else
        {
            shootTimer = 0f;
            gun.onFireUp();
        }


        //Update state
        if (Vector3.Distance(player.position, transform.position) <= range && !(currentState == State.Stunned))
        {
            currentState = State.Attack;
        }
        else if (currentState == State.Attack && !(currentState == State.Stunned))
        {
            currentState = State.Patrol;
        }

        //Different States
        switch(currentState)
        {
            case State.Idle:
                idle();
                break;
            case State.Patrol:
                patrol();
                break;
            case State.Attack:
                attack();
                break;
            case State.Stunned:
                stunned();
                break;
        }

        if (health <= 0)
        {
            die();
        }
    }

    public void grenadeHit(Vector3 explosionCenter, float explosionForce, float explosionRadius)
    {
        health -= 34;
        agent.enabled = false;
        rb.isKinematic = false;
        rb.inertiaTensor = new Vector3(0, 0, 0);
        rb.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);
        currentState = State.Stunned;
        stunTimer = 0f;
    }
    void attack()
    {
        agent.stoppingDistance = range/2;
        agent.speed = speed/2;
        agent.destination = player.position;
        gun.onClick(facingForwards, agent.velocity.x, "Bullet");
    }

    void patrol()
    {
        agent.stoppingDistance = 0f;
        agent.speed = speed;
        if (agent.remainingDistance == 0 && currentTarget == pointB)
        {
            currentTarget = pointA;
            currentState = State.Idle;
        }
        else if (agent.remainingDistance == 0)
        {
            currentTarget = pointB;
            currentState = State.Idle;
        }
        agent.destination = currentTarget.position;
    }
    
    void idle()
    {
        agent.speed = 0;
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDelay)
        {
            idleTimer = 0;
            currentState = State.Patrol;
        }
    }

    void stunned()
    {
        stunTimer += Time.deltaTime;
        if (stunTimer > stunDelay)
        {
            agent.enabled = true;
            rb.isKinematic = true;
            agent.destination = currentTarget.position;
            currentState = State.Patrol;
        }
    }

    void die()
    {
        //DROP GUN
        gun.transform.parent = null;
        gun.gameObject.tag = "Weapon";
        gun.switchToAuto();
        gun.transform.position = this.transform.position + new Vector3(0, 1, 0);
        gun.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        //SPAWN LOOT AND DELETE
        lootSplash.spawnLoot();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health -= collision.gameObject.GetComponent<BulletScript>().damage;
            Destroy(collision.gameObject);
        }
    }
}
