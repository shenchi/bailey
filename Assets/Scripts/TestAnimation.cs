using UnityEngine;
using System.Collections;

public class TestAnimation : MonoBehaviour
{
    public Vector2 vel;
    private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = vel;
    }
}
