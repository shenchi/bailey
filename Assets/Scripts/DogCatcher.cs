using UnityEngine;
using System.Collections;

public class DogCatcher : MonoBehaviour
{
    private float LostTime;
    private Vector3 destination;
    public GameObject target;
    private bool isArrived;
    public int hp;
    public int attack;
    private float attackCd;
    private float RedTime;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        LostTime = 0;
        attackCd = 1;
        isArrived = true;
        RedTime = -1;
        hp = Random.Range(5, 10);
        attack = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (RedTime >= 0)
        {
            RedTime -= Time.deltaTime;
            if (RedTime <= 0)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        
        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (isArrived)
            {
                if (distance < 20)
                {
                    //attack
                    if (target.activeInHierarchy)
                    {
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
                    else {
                        LostTime += Time.deltaTime;
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
            if (LostTime > 10) {
                Destroy(gameObject);
                GameObject.Find("GameController").GetComponent<GameEventManager>().hasGeneDogCatcherNum--;
                GameObject.Find("GameController").GetComponent<GameEventManager>().thisWaveNum--;
                GameObject.Find("Canvas").GetComponent<UIManager>().SetStar(0);
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
            if (target.GetComponent<PlayerController>().curhp > 0)
            {
                go.GetComponent<PlayerController>().TakeAttack(attack);
            }
            else {
                target = null;
            }
            
        }
        else if (target.tag == "OtherDog")
        {
            if (target.GetComponent<OtherDog>().curhp > 0)
            {
                go.GetComponent<OtherDog>().TakeAttack(attack, gameObject);
            }
            else {
                target = GameObject.FindGameObjectWithTag("Player");
            }
           
        }
    }
    public void TakeAttack(int damage, GameObject go)
    {
        RedTime = 0.3f;
        GetComponent<SpriteRenderer>().color = Color.red;
        target = go;
        hp = Mathf.Clamp(hp - damage, 0, hp);
        if (hp <= 0)
        {
            GameObject.Find("GameController").GetComponent<GameEventManager>().alldogcatchernum--;
            GameObject.Find("GameController").GetComponent<GameEventManager>().thisWaveNum--;
            Destroy(gameObject);
        }
    }
}
