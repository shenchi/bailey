using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Tiled2Unity.TiledMap map;

    public float speedFactor = 100.0f;

    private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        //transform.Translate(vel * Time.deltaTime * speedFactor);
        rigid.velocity = vel * speedFactor;
    }
}
