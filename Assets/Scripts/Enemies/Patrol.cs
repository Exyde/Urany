﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform PathHolder;
    Vector3[] waypoints;
    public bool cycle;

    public float moveSpeed = 2f;
    public float waitTime = .1f;
    public float dir;

    public bool isPaused;

    Enemy entity;

    void Start()
    {
        waypoints = new Vector3[PathHolder.childCount];
         for (int i  = 0; i < waypoints.Length; i++)
		{
            waypoints[i] = PathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y, 0);
		}

        entity = GetComponent<Enemy>();

        StartPatrol();

    }

    IEnumerator FollowPath(Vector3[] waypoints)
	{
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];

        Turn(targetWaypoint);

        while (true)
		{
            while (isPaused)
			{
                yield return null;
			}

            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);
            entity.isMoving = true;

            if (transform.position == targetWaypoint)
			{
                entity.isMoving = false;
                GetComponent<Enemy>().isMoving = false;
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];

                Turn(targetWaypoint);

                yield return new WaitForSeconds(waitTime);
			}

            yield return null;
		}
	} 

    public void Stop()
	{
        StopAllCoroutines();
	}

    public void StartPatrol()
	{
        StartCoroutine(FollowPath(waypoints));
    }

    public void Turn(Vector3 target)
    {
        Vector3 side = (target - transform.position).normalized;
        GetComponent<SpriteRenderer>().flipX = (side.x == 1) ? true : false;
        dir = side.x;
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
