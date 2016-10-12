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
                //pickedItem = 
                pickedItem.GetComponent<ItemProperty>().isPickedUp = true;
                pickedItem.GetComponent<ItemProperty>().previousOwner = pickedItem.GetComponent<ItemProperty>().owner;
                pickedItem.GetComponent<ItemProperty>().owner = gameObject;
            }

            if (Input.GetButtonDown("Pick") && hasItem == true)
            {
                pickedItem.GetComponent<ItemProperty>().isPickedUp = false;
                pickedItem = null;
            }
        }

        if(gameObject.transform.position == dogHouse.transform.position)
        {
            dogHouse.GetComponent<DogHouseInventory>().openInventory();
        }

        if(dogHouse.GetComponent<DogHouseInventory>().isOpened == true && Input.GetButtonDown("Pick"))
        {
            dogHouse.GetComponent<DogHouseInventory>().closeInventory();
        }
        //transform.Translate(vel * Time.deltaTime * speedFactor);
        rigid.velocity = vel * speedFactor;
        velX = rigid.velocity.x;
        velY = rigid.velocity.y;

        anim.SetFloat(animVelXId, rigid.velocity.x);
        anim.SetFloat(animVelYId, rigid.velocity.y);
    }
}
