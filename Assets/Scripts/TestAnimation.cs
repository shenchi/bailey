using UnityEngine;
using System.Collections;

public class TestAnimation : MonoBehaviour
{
    //public Vector2 vel;
    //private Rigidbody2D rigid;
    public GameObject followee;
    public PathFinder pathFinder;

    public bool go = false;

    // Use this for initialization
    void Start()
    {
        //rigid = GetComponent<Rigidbody2D>();
        pathFinder = GetComponent<PathFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.velocity = vel;
        //if (go)
        //{
        //    go = false;
        //    GetComponent<PathFinder>().FindPath(6, 6);
        //}
        if (null == followee || null == pathFinder)
            return;
        pathFinder.FindPath(followee.transform.position.x, followee.transform.position.y);
    }
}
