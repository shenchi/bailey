using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathFinder))]
public class Follower : MonoBehaviour
{
    public GameObject followee;
    public PathFinder pathFinder;
    
    // Use this for initialization
    void Start()
    {
        //rigid = GetComponent<Rigidbody2D>();
        pathFinder = GetComponent<PathFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (null == followee || null == pathFinder)
            return;
        pathFinder.FindPath(followee.transform.position.x, followee.transform.position.y);
    }
}
