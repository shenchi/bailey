using UnityEngine;
using System.Collections;

public class DogCatcher : MonoBehaviour
{
    private float LostTime;
    private Vector3 destination;
    private bool isFollowing;
    private GameObject target;
    private bool isArrived;
    public int hp;
    public int attack;
    private float attackCd;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        isFollowing = true;
        LostTime = 0;
        attackCd = 1;
        isArrived = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (isArrived)
        {
            if (distance < 20)
            {
                //attack
                if (attackCd == 1)
                {
                    attackCd -= Time.deltaTime;
                    Attack(target);
                }
                else
                {
                    attackCd -= Time.deltaTime;
                    if (attackCd <= 0)
                    {
                        attackCd = 1;
                    }
                }
            }
            else
            {
                destination = target.transform.position;
                GetComponent<PathFinder>().FindPath(destination.x, destination.y);
                isArrived = false;
            }
            
        }
        else
        {
            if (Vector3.Distance(transform.position, destination) < 15)
            {
                isArrived = true;
            }
        }


        //if (isFirst) {
        //    destination = target.transform.position;
        //    GetComponent<PathFinder>().FindPath(destination.x, destination.y);
        //    isFirst = false;
        //}

        //if (target == null) {
        //    target= GameObject.FindGameObjectWithTag("Player");
        //}
        ////goto first crime place
        //if (Vector3.Distance(transform.position, destination) < 30)
        //{
        //    destination = target.transform.position;
        //    isFollowing = false;
        //    if (attackCd == 1)
        //    {
        //        attackCd -= Time.deltaTime;
        //        Attack(target);
        //    }
        //    else {
        //        attackCd -= Time.deltaTime;
        //        if (attackCd <= 0) {
        //            attackCd = 1;
        //        }
        //    }
        //}
        //else
        //{
        //    isFollowing = true;
        //}
        //if (isFollowing)
        //{
        //    if (Vector3.Distance(transform.position, destination) < 10)
        //    {
        //        if (target.activeInHierarchy)
        //        {
        //            destination = target.transform.position;
        //            GetComponent<PathFinder>().FindPath(destination.x, destination.y);
        //        }
        //        else
        //        {
        //            LostTime += Time.deltaTime;
        //        }
        //    }
        //}
        //if (LostTime > 10) {
        //    Destroy(gameObject);
        //}
        //every time reach place find again

    }
    void Attack(GameObject go)
    {
        if (target.tag == "Player")
        {
            go.GetComponent<PlayerController>().TakeAttack(attack);
        }
        else if (target.tag == "OtherDog")
        {
            go.GetComponent<OtherDog>().TakeAttack(attack, gameObject);
        }
    }
    public void TakeAttack(int damage, GameObject go)
    {
        target = go;
        hp = Mathf.Clamp(hp - damage, 0, hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
