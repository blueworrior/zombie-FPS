using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int currentWaypoint = 0;

    public Transform player; // Drag your FPS player here in the Inspector
    public float chaseDistance = 10f;

    private Animator animator;

    public bool canAttack = true;
    public float damageAmount = 20f;

    [SerializeField]
    float attackTime = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GoToNextWaypoint();
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[currentWaypoint].position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        //player dies
        if (PlayerHealth.singleton.isDead)
        {
            DisableEnemy();
            return;
        }


        //chasing
        if (distanceToPlayer < chaseDistance)
        {
            // Player is close enough, chase him
            agent.destination = player.position;
            agent.speed = 2f;

            // Immediately switch to run animation
            animator.SetBool("isRunning", true);
        }
        else
        {
            // Patrol speed
            agent.speed = 0.5f;

            // Resume patrol
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                GoToNextWaypoint();
            }

            // Immediately switch to walk animation
            animator.SetBool("isRunning", false);
        }

        //biting and attacking the player
        if (distanceToPlayer < 1f && canAttack) // 🟡 biting distance, adjust as needed!
        {
            // Stop movement
            agent.isStopped = true;

            // Switch to biting animation - ATTACKING THE PLAYER
            animator.SetBool("isBiting", true);
            animator.SetBool("isRunning", false);
            StartCoroutine(AttackTime());        // added by imama
        }
        else if (distanceToPlayer < chaseDistance)
        {
            // Player is close enough, chase him
            agent.isStopped = false;
            agent.destination = player.position;
            agent.speed = 2f;

            // Run animation
            animator.SetBool("isRunning", true);
            animator.SetBool("isBiting", false);
        }
        else
        {
            // Patrol speed
            agent.isStopped = false;
            agent.speed = 0.5f;

            // Resume patrol
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                GoToNextWaypoint();
            }

            // Walk animation
            animator.SetBool("isRunning", false);
            animator.SetBool("isBiting", false);
        }
    }

    //added by Imama

public void DisableEnemy()
{
    canAttack = false;
    agent.isStopped = true;
    animator.SetBool("isBiting", false);
    animator.SetBool("isRunning", false);
    // Optional: disable this script if needed
    // this.enabled = false;
}


    //added by imama
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.singleton.PlayerDamage(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;

    }
}
