using UnityEngine;
using System.Collections;

public class AnimController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigid;

    private int velXId;
    private int velYId;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        velXId = Animator.StringToHash("velX");
        velYId = Animator.StringToHash("velY");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(velXId, rigid.velocity.x);
        anim.SetFloat(velYId, rigid.velocity.y);
    }
}
