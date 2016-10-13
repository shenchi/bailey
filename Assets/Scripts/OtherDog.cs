using UnityEngine;
using System.Collections;

public class OtherDog : MonoBehaviour
{
    public int attack;
    public int curhp;
    public int maxhp;
    public GameObject FriendsTo;
    private bool IsAttack;
    private float LastRandomTime;
    public float RandomTime;
    public GameObject AttackTarget;
    public Vector3 Destination;
    private float AttackCd;
    private GameObject[] RandomPoint;
    private bool IsFollow;
    // Use this for initialization
    void Start()
    {
        IsFollow = false;
        LastRandomTime = 0;
        AttackCd = 3;
        RandomPoint = GameObject.FindGameObjectsWithTag("RandomPoint");
        int i=Random.Range(0, 100);
        if (i < 20) {
            BeginToAttack(GameObject.FindGameObjectWithTag("Player"));
        }
        this.tag = "OtherDog";
    }

    // Update is called once per frame
    void Update()
    {
        //randomly move 
        //randomly attack
        //if player help this dog, then become friend, then go randomly
        //if player call fo help, hall his friends come together to help attack the other dog
        //give sth to the dog then become friends
        if (curhp <= 0) {
            Destroy(gameObject);
        }
        if (!IsAttack)
        {
            if (Mathf.FloorToInt(Time.timeSinceLevelLoad) - LastRandomTime >= RandomTime)
            {
                LastRandomTime = Time.timeSinceLevelLoad;
                RandomDestination();
            }
        }
        else
        {
            if (AttackTarget != null)
            {

                if (Vector3.Distance(transform.position, AttackTarget.transform.position) > 30)//follow
                {
                    if (IsFollow == false)
                    {
                        GetComponent<PathFinder>().FindPath(AttackTarget.transform.position.x, AttackTarget.transform.position.y);
                        Destination = AttackTarget.transform.position;
                        IsFollow = true;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, Destination) < 10)
                        {
                            IsFollow = false;
                        }
                    }

                }
                else
                {//attack
                    if (AttackCd == 3)
                    {
                        Attack();
                        AttackCd -= Time.deltaTime;
                    }
                    else
                    {
                        AttackCd -= Time.deltaTime;
                        if (AttackCd <= 0) {
                            AttackCd = 3;
                        }                      
                    }
                }
            }
        }
    }
    public void BecomeFriends(GameObject dog)
    {
        FriendsTo = dog;
    }
    public void HelpFriend()
    {

    }
    public void RandomDestination()
    {
        int r = Random.Range(0, RandomPoint.Length);
        Destination = RandomPoint[r].transform.position;
        GetComponent<PathFinder>().FindPath(Destination.x, Destination.y);
    }
    public void BeginToAttack(GameObject target)
    {
        IsAttack = true;
        AttackTarget = target;
    }
    void Attack()
    {
        if (AttackTarget.tag == "OtherDog")
        {
            if (AttackTarget.GetComponent<OtherDog>().curhp > 0)
            {
                AttackTarget.GetComponent<OtherDog>().TakeAttack(this.attack, gameObject);
            }
            else
            {
                IsAttack = false;
                curhp = maxhp;
                AttackTarget = null;
            }
        }
        else if (AttackTarget.tag == "Player")
        {
            if (AttackTarget.GetComponent<PlayerController>().curhp > 0)
            {
                AttackTarget.GetComponent<PlayerController>().TakeAttack(this.attack);
            }
            else
            {
                IsAttack = false;
                curhp = maxhp;
                AttackTarget = null;
            }
        }
    }
    public void TakeAttack(int damage, GameObject go)
    {
        BeginToAttack(go);
        curhp = Mathf.Clamp(curhp - damage, 0, maxhp);
    }
    public void HelpFirend(GameObject target) {
        BeginToAttack(target);
    }
}
