using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speedFactor = 100.0f;
    public bool hasItem;
    public GameObject pickedItem;
    public string myName;
    public GameObject dogHouse;

    public float velX;
    public float velY;

    private Rigidbody2D rigid;
    private Animator anim;
    private int animVelXId;
    private int animVelYId;
    public GameObject NowOnObject;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        hasItem = false;
        myName = GetComponent<NPCProperty>().myName;
        anim = GetComponent<Animator>();
        animVelXId = Animator.StringToHash("velX");
        animVelYId = Animator.StringToHash("velY");
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

        if (dogHouse.GetComponent<DogHouseInventory>().isOpened == false)
        {
            if (Input.GetButtonDown("Pick") && hasItem == false)
            {
                //Detect Item
                if (NowOnObject != null)
                {
                    pickedItem = NowOnObject;
                    NowOnObject.GetComponent<ItemProperty>().PickUp(gameObject);
                    hasItem = true;
                }
            }
            else if (Input.GetButtonDown("Pick") && hasItem == true)
            {
                NowOnObject.GetComponent<ItemProperty>().Drop(gameObject);
                pickedItem = null;
                hasItem = false;
            }
        }

        //if (gameObject.transform.position == dogHouse.transform.position)
        //{
        //    dogHouse.GetComponent<DogHouseInventory>().openInventory();
        //}

        //if (dogHouse.GetComponent<DogHouseInventory>().isOpened == true && Input.GetButtonDown("Pick"))
        //{
        //    dogHouse.GetComponent<DogHouseInventory>().closeInventory();
        //}
        //transform.Translate(vel * Time.deltaTime * speedFactor);
        rigid.velocity = vel * speedFactor;
        velX = rigid.velocity.x;
        velY = rigid.velocity.y;

        anim.SetFloat(animVelXId, rigid.velocity.x);
        anim.SetFloat(animVelYId, rigid.velocity.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            NowOnObject = other.gameObject;
        }
    }

}
