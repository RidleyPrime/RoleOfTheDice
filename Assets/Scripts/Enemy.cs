using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform movepoint;
    NavMeshAgent agent;

    private Transform playerTransform;
    private Animator animator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("PlayerArmature").transform;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        agent.destination = playerTransform.position;

        if (agent.remainingDistance < 2f)
        {
            agent.isStopped = true;
        }

        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Running",true);
        }
        else
        {
            animator.SetBool("Running",false);
            animator.SetBool("Idle",true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetTrigger("Attack");
        }
    }
}
