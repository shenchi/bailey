using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speedFactor = 100.0f;
    public bool hasItem;
    public GameObject pickedItem;
    public string myName;
    public GameObject dogHouse;

    public int CrimeIndex;
    public int attack;
    public int curhp;
    public int maxhp;
    public float attackCd;
    private bool IsAttackAnim;
    private float AttackAnim;
    private Vector3 direction;
    private float RedTime;
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
        CrimeIndex = 0;
        attackCd = 1;
        AttackAnim = 0;
        direction = Vector3.zero;
        gameObject.tag = "DogCatcher";
        RedTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            TakeAttack(1);
        }
        if (RedTime >= 0) {
            RedTime -= Time.deltaTime;
            if (RedTime <= 0) {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (IsAttackAnim) {           
            if (AttackAnim == 0) {
                transform.position +=  3*direction;
            }           
            AttackAnim += Time.deltaTime;
            if (AttackAnim >= 0.1) {
                transform.position -= 3*direction;
                IsAttackAnim = false;
                AttackAnim = 0;
            }
        }
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
        if (vel.x != 0||vel.y!=0)
        {
            direction = vel.normalized;
        }
        if (dogHouse.GetComponent<DogHouseInventory>().isOpened == false)
        {
            if (Input.GetButtonDown("Pick") && hasItem == false)
            {
                //Detect Item
                if (NowOnObject != null && NowOnObject.tag == "Item" && NowOnObject.GetComponent<ItemProperty>().isPickedUp == false)
                {
                    pickedItem = NowOnObject;
                    NowOnObject.GetComponent<ItemProperty>().PickUp(gameObject);
                    hasItem = true;
                    GetComponentInChildren<TextMesh>().text = "";
                }
            }
            else if (Input.GetButtonDown("Pick") && hasItem == true)
            {
                pickedItem.GetComponent<ItemProperty>().Drop(gameObject);
                pickedItem = null;
                hasItem = false;
            }
            if (Input.GetButtonDown("Return") && hasItem == true && NowOnObject.tag == "NPC")//return to NPC
            {
                if (pickedItem.GetComponent<ItemProperty>().Return(NowOnObject))
                {
                    pickedItem = null;
                    hasItem = false;
                    GetComponentInChildren<TextMesh>().text = "";
                }
            }
            if (NowOnObject != null && Input.GetButtonDown("Attack") && (NowOnObject.tag == "NPC"|| NowOnObject.tag == "OtherDog" || NowOnObject.tag == "DogCatcher") && attackCd == 1)
            {
                IsAttackAnim = true;
                Attack(NowOnObject);
                attackCd -= Time.deltaTime;
            }
            if (attackCd < 1)
            {
                attackCd -= Time.deltaTime;
                if (attackCd <= 0)
                {
                    attackCd = 1;
                }
            }
            if (NowOnObject != null && Input.GetButtonDown("Hide") && NowOnObject.tag == "Hide")
            {
                GetComponentInChildren<TextMesh>().text = "";
                NowOnObject.GetComponent<HidePlace>().HidePlayer(gameObject);               
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
        if (other.tag == "Item" && other.GetComponent<ItemProperty>().isPickedUp == false)
        {
            GetComponentInChildren<TextMesh>().text = "Press E to pick up";
            NowOnObject = other.gameObject;
        }
        else if (other.tag == "NPC")
        {
            NowOnObject = other.gameObject;
            if (hasItem)
            {
                GetComponentInChildren<TextMesh>().text = "Press R to return";
            }
        }
        else if (other.tag == "Hide")
        {
            NowOnObject = other.gameObject;
            GetComponentInChildren<TextMesh>().text = "Press H to hide";
        }
        else if (other.tag == "OtherDog")
        {
            NowOnObject = other.gameObject;
        }
        else if (other.tag == "DogCatcher")
        {
            NowOnObject = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInChildren<TextMesh>().text = "";
        NowOnObject = null;
    }
    void Attack(GameObject other)
    {
        print(other.tag);
        if (other.tag == "NPC")
        {
            other.GetComponent<NPCProperty>().TakeAttack(this.attack);
            CrimeIndex++;
        }
        else if (other.tag == "OtherDog")
        {
            other.GetComponent<OtherDog>().TakeAttack(this.attack, gameObject);
        }
        else if (other.tag == "DogCatcher")
        {
            other.GetComponent<DogCatcher>().TakeAttack(this.attack, gameObject);
        }
    }
    public void TakeAttack(int damage)
    {
        RedTime = 0.3f;
        GetComponent<SpriteRenderer>().color = Color.red;
        curhp = Mathf.Clamp(curhp - damage, 0, maxhp);
    }

}
