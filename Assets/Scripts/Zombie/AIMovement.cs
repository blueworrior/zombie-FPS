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
    }
}
