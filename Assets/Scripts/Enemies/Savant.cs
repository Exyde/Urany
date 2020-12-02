using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savant : MonoBehaviour
{
    public Transform PathHolder;
    Vector3[] waypoints;
    public bool cycle;

    public float moveSpeed = 2f;
    public float waitTime = .1f;

    public enum State
	{
        Idle, 
        Patrol
	}

    public State state;
    void Start()
    {
        waypoints = new Vector3[PathHolder.childCount];
         for (int i  = 0; i < waypoints.Length; i++)
		{
            waypoints[i] = PathHolder.GetChild(i).position;
		}
        
        state = State.Patrol;

        StartCoroutine(FollowPath(waypoints));
    }

    IEnumerator FollowPath(Vector3[] waypoints)
	{
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];

        while(true)
		{
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);
            print(targetWaypoint);

            if (transform.position == targetWaypoint)
			{
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
			}

            yield return null;
		}
	}
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;

        Vector3 startPos = PathHolder.GetChild(0).position;
        Vector3 previousPos = startPos;
        
        foreach(Transform t in PathHolder)
		{
            Gizmos.DrawSphere(t.position, .1f);
            Gizmos.DrawLine(previousPos, t.position);
            previousPos = t.position;
		}

        Gizmos.DrawLine(previousPos, startPos);
    }
}
