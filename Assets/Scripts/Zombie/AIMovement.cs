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

    // If the player is dead
    if (PlayerHealth.singleton.isDead)
    {
        DisableEnemy();
        return;
    }

    // If in biting range
    if (distanceToPlayer < 1f)
    {
        // Stop moving
        agent.isStopped = true;

        // Keep biting animation active no matter what
        animator.SetBool("isBiting", true);
        animator.SetBool("isRunning", false);

        // Only start attack coroutine if not already attacking
        if (canAttack)
        {
            StartCoroutine(AttackTime());
        }

        // Return early so no other logic runs
        return;
    }

    // If in chase range but not biting
    if (distanceToPlayer < chaseDistance)
    {
        agent.isStopped = false;
        agent.destination = player.position;
        agent.speed = 2f;

        // Running animation
        animator.SetBool("isRunning", true);
        animator.SetBool("isBiting", false);
    }
    else
    {
        agent.isStopped = false;
        agent.speed = 0.5f;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            GoToNextWaypoint();
        }

        // Walking animation
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
