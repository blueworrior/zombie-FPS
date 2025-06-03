using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class WaypointPlacer : MonoBehaviour
{
    public GameObject waypointPrefab;    // Drag the prefab here
    public Transform waypointParent;     // Drag the Waypoints parent here

    private void Update()
    {
        // Only in the Scene view and Edit Mode (not Play Mode)
        if (!Application.isPlaying && Event.current != null && Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            // Create a ray from the mouse position
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Place the waypoint at the hit point
                GameObject waypoint = PrefabUtility.InstantiatePrefab(waypointPrefab) as GameObject;
                waypoint.transform.position = hit.point;
                waypoint.transform.SetParent(waypointParent);
                waypoint.name = "Waypoint_" + waypointParent.childCount;

                Debug.Log("Waypoint placed at: " + hit.point);
                Event.current.Use(); // Mark event as used
            }
        }
    }
}