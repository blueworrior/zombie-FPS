using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsManager : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();
    public bool isMoving;
    public int wayPointIndex;
    public float moveSpeed;
    public float rotationSpeed = 5f; // Rotation speed for smooth turning

    void Start()
    {
        StartMoving();
    }

    public void StartMoving() {
        wayPointIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving || wayPoints.Count == 0) {
            return;
        }

        // Move towards the next waypoint
        Vector3 targetPosition = wayPoints[wayPointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Calculate the direction to look at
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            // Create the target rotation
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        // Check if we reached the waypoint
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance <= 0.05f) {
            wayPointIndex++;

            // If we've reached the end of the list, stop moving or loop back
            if (wayPointIndex >= wayPoints.Count) {
                isMoving = false;
            }
        }
    }
}
