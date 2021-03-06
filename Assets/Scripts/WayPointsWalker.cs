﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PathFinder))]
public class WayPointsWalker : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int currentWayPoint = -1;

    private PathFinder pathFinder;
    
    // Use this for initialization
    void Start()
    {
        pathFinder = GetComponent<PathFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathFinder.CurrentState != PathFinder.State.InPath)
        {
            bool allFinished = true;

            if (currentWayPoint >= 0)
            {
                WayPointTask[] task = wayPoints[currentWayPoint].GetComponents<WayPointTask>();
                if (task.Length > 0)
                {
                    for (int i = 0; i < task.Length; i++)
                    {
                        if (task[i].enabled == false)
                        {
                            task[i].enabled = true;
                            allFinished = false;
                            break;
                        }
                        if (!task[i].isFinished)
                        {
                            allFinished = false;
                            break;
                        }
                    }
                }
                
            }

            if (allFinished && null != wayPoints && wayPoints.Count > 0 && currentWayPoint < wayPoints.Count - 1)
            {
                currentWayPoint++;
                pathFinder.FindPath(wayPoints[currentWayPoint].transform);
            }
        }
    }

    public void InitWayPoints(IEnumerable<Transform> wp)
    {
        wayPoints = new List<Transform>(wp);
    }

    public void AddWayPoint(Transform wayPoint)
    {
        if (null == wayPoints)
            wayPoints = new List<Transform>();
        wayPoints.Add(wayPoint);
    }
}
