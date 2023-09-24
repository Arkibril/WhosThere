using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Patrol : MonoBehaviour
{
    public float patrolSpeed = 2f;                                  // The nav mesh agent's speed when patrolling.
    public List<Transform> waypoints = new List<Transform>();
    public List<int> visitedIndices = new List<int>();             // List to store the indices of visited waypoints
    public UnityEngine.AI.NavMeshAgent _agent;

    /// <summary>
    /// Patrol state. Enables or Disables the enemy patrol between the waypoints
    /// </summary>
    public enum patrolState { NOPATROL, PATROL };
    public patrolState state = patrolState.PATROL;

    private int currentWaypointIndex = 0;

    void Awake()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject obj in objectsWithTag)
        {
            Transform waypointTransform = obj.transform;
            if (waypointTransform != null)
            {
                waypoints.Add(waypointTransform);
            }
        }

        // inits
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // cache the number of waypoints
        if (waypoints.Count > 0)
        {
            _agent.SetDestination(waypoints[currentWaypointIndex].position);
            visitedIndices.Add(currentWaypointIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // call patrol if there is no path assigned to the navmeshagent
        if (state == patrolState.PATROL)
        {
            if (!_agent.hasPath && !_agent.pathPending)
            {
                GoToNextWaypoint();
            }
        }
        else if (state == patrolState.NOPATROL)
        {
            if (!_agent.hasPath && !_agent.pathPending)
            {
                GoToNextWaypoint();
            }
        }
    }

    private void GoToNextWaypoint()
    {
        // Find the next unvisited waypoint index
        int nextWaypointIndex = FindNextUnvisitedWaypointIndex();

        // If all waypoints are visited, return to the first one
        if (nextWaypointIndex == -1)
        {
            nextWaypointIndex = 0;

            // Reset the visitedIndices list
            visitedIndices.Clear();
        }

        // Mark the current waypoint as visited
        visitedIndices.Add(currentWaypointIndex);

        // Go to the next waypoint in order
        currentWaypointIndex = nextWaypointIndex;
        _agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private int FindNextUnvisitedWaypointIndex()
    {
        // Find the next unvisited waypoint index
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (!visitedIndices.Contains(i))
            {
                return i;
            }
        }

        // If all waypoints are visited, return -1
        return -1;
    }
}
