using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { Idle, Patrol, Attack };
public class TempEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GunScript gun;
    [SerializeField] private Transform player;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float speed = 3.5f;
    public float health = 1;
    [SerializeField] private State currentState;

    //tempvariable since gunscript is broken right now
    bool facingForwards;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        currentState = State.Patrol;
        agent = GetComponent<NavMeshAgent>();

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
        else
        {
            facingForwards = false;
        }

        if (Vector3.Distance(player.position, transform.position) <= 10)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Patrol;
        }

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
        }

        if (health <= 0)
        {
            die();
        }
    }

    void attack()
    {
        agent.speed = speed/2;
        agent.destination = player.position;
        gun.FireGun(facingForwards, agent.velocity.x);
    }

    void patrol()
    {
        agent.speed = speed;
        if (agent.remainingDistance == 0 && currentTarget == pointB)
        {
            currentTarget = pointA;
            
        }
        else if (agent.remainingDistance == 0)
        {
            currentTarget = pointB;
        }
        agent.destination = currentTarget.position;
    }
    
    void idle()
    {
        agent.speed = 0;
    }

    void die()
    {
        Destroy(this.gameObject);
    }
}
