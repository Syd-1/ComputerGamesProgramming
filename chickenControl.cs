using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chickenControl : MonoBehaviour
{
    float amount;
    public GameObject player;
    private NavMeshAgent chicken;
    private Rigidbody body;
    private float distApart;
    public float runAt;
    public float walkAt;
    private Vector3 randomdestination;
    private bool newPathNeeded;
    Animator anim;
    Vector3 direction;

    void Start()
    {
        chicken = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        Vector3 randomdestination = transform.position;
        newPathNeeded = true;
    }

    void Update()
    {
        distApart = Vector3.Distance(player.transform.position, transform.position);
        direction = ((transform.position) - (player.transform.position)).normalized;
        if(Vector3.Distance(transform.position, randomdestination) < 2.0f)
        {
            newPathNeeded = true;
        }
        if (distApart < runAt)
        {
            run();
        }
        if (newPathNeeded)
        {
            if (distApart < walkAt)
            {
                walk();
            }
            else if (distApart <= (walkAt + 3))
            {
                lookAround();
            }
            else
            {
                eat();
            }
        }        
    }

    void run()
    {
        anim.SetBool("Eat", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", true);
        chicken.speed = 8;
        newPathNeeded = true;
        chicken.destination = transform.position + direction;
    }

    void walk()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Turn Head", false);
        anim.SetBool("Walk", true);
        setDestination();
    }

    void lookAround()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Eat", false);
        anim.SetBool("Turn Head", true);
    }

    void eat()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Turn Head", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Eat", true);
    }

    void setDestination()
    {
        NavMeshHit hit;
        for (int i = 0; i < 30; i++)
        {
            randomdestination = transform.position + Random.insideUnitSphere * 20f;
            if (NavMesh.SamplePosition(randomdestination, out hit, 1.0f, NavMesh.AllAreas))
            {
                chicken.speed = 1;
                chicken.destination = randomdestination;
                newPathNeeded = false;
                break;
            }
        }
    }

    void onCollisionEnter(Collider coll)
    {
        if (coll.gameObject.tag == "chicken")
        {
            lookAround();
        }
    }

}
