using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speedFactor = 100.0f;
    public bool hasItem;
    public GameObject pickedItem;
    public string myName;

    private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        hasItem = false;
        myName = GetComponent<NPCProperty>().myName;
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 vel = Vector3.zero;

        if (Mathf.Abs(xAxis) > Mathf.Abs(yAxis))
        {
            vel.x = xAxis;
        }
        else
        {
            vel.y = yAxis;
        }

        if(Input.GetButtonDown("Pick") && hasItem == false)
        {
            //Detect Item
            //pickedItem = 
            pickedItem.GetComponent<ItemProperty>().isPickedUp = true;
            pickedItem.GetComponent<ItemProperty>().owner = gameObject;
        }

        if(Input.GetButtonDown("Pick") && hasItem == true)
        {
            pickedItem.GetComponent<ItemProperty>().isPickedUp = false;
            pickedItem = null;

        }
        //transform.Translate(vel * Time.deltaTime * speedFactor);
        rigid.velocity = vel * speedFactor;
    }
}
