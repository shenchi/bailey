﻿using UnityEngine;
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
    private float RedTime;
    // Use this for initialization
    void Start()
    {
        RandomTime = 20f;
        IsFollow = false;
        LastRandomTime = 0;
        AttackCd = 2;
        RandomPoint = GameObject.FindGameObjectsWithTag("RandomPoint");
        int i=Random.Range(0, 100);
        if (i < 20) {
            BeginToAttack(GameObject.FindGameObjectWithTag("Player"));
        }
        this.tag = "OtherDog";
        RedTime = -1;

        maxhp = Random.Range(1, 10);
        curhp = maxhp;
        attack = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //randomly move 
        //randomly attack
        //if player help this dog, then become friend, then go randomly
        //if player call fo help, hall his friends come together to help attack the other dog
        //give sth to the dog then become friends
        if (RedTime >= 0)
        {
            RedTime -= Time.deltaTime;
            if (RedTime <= 0)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
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
                    if (AttackCd == 2)
                    {
                        Attack();
                        AttackCd -= Time.deltaTime;
                    }
                    else
                    {
                        AttackCd -= Time.deltaTime;
                        if (AttackCd <= 0) {
                            AttackCd = 2;
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
        if (!AttackTarget.activeInHierarchy) {
            return;
        }
        if (AttackTarget.tag == "OtherDog")
        {
            if (AttackTarget.GetComponent<OtherDog>().curhp > 0)
            {
                AttackTarget.GetComponent<OtherDog>().TakeAttack(this.attack, gameObject);
            }
            else
            {
                IsAttack = false;
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
                AttackTarget = null;
            }
        }
        else if (AttackTarget.tag == "NPC") {
            if (AttackTarget.GetComponent<NPCProperty>().hp > 0)
            {
                AttackTarget.GetComponent<NPCProperty>().TakeAttack(this.attack);
            }
            else
            {
                IsAttack = false;
                AttackTarget = null;
            }

        }
        else if (AttackTarget.tag == "DogCatcher")
        {
            if (AttackTarget.GetComponent<DogCatcher>().hp > 0)
            {
                AttackTarget.GetComponent<DogCatcher>().TakeAttack(this.attack,gameObject);
            }
            else
            {
                IsAttack = false;
                AttackTarget = null;
            }
        }
    }
    public void TakeAttack(int damage, GameObject go)
    {
        RedTime = 0.3f;
        GetComponent<SpriteRenderer>().color = Color.red;
        BeginToAttack(go);
        curhp = Mathf.Clamp(curhp - damage, 0, maxhp);
        if (curhp <= 0)
        {
            GameObject.Find("GameController").GetComponent<GameEventManager>().maxDogNum++;
            Destroy(gameObject);
        }
    }
    public void HelpFirend(GameObject target) {
        BeginToAttack(target);
    }
}
