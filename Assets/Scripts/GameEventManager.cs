using UnityEngine;
using System.Collections;

public class GameEventManager : MonoBehaviour
{
    public GameObject dogCatcher;
    public GameObject wildDog;
    public GameObject Bone;
    public GameObject UImanager;
    public int maxDogNum;
    public int alldogcatchernum;
    public int maxDogCatcherNum;
    public int hasGeneDogCatcherNum;
    public float dogGenerateTime;
    private float lastgenetime;
    private GameObject[] RandomPoints;
    public bool isThisWaveOver;
    public int thisWaveNum;
    // Use this for initialization
    void Start()
    {
        hasGeneDogCatcherNum = 0;
        RandomPoints = GameObject.FindGameObjectsWithTag("RandomPoint");
        alldogcatchernum = maxDogCatcherNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - lastgenetime > 5&&maxDogNum>0) {
            lastgenetime = Time.timeSinceLevelLoad;
            int r = Random.Range(0, RandomPoints.Length);
            Instantiate(wildDog, RandomPoints[r].transform.position, Quaternion.identity);
            r = Random.Range(0, RandomPoints.Length);
            Instantiate(Bone, RandomPoints[r].transform.position, Quaternion.identity);
            maxDogNum--;
        }
        if (alldogcatchernum == 0) {
            //win
        }
        if (thisWaveNum <= 0) {
            isThisWaveOver = true;
            UImanager.GetComponent<UIManager>().SetStar(0);
        }
    }
    public void CallDogCatcher(int num) {
        GameObject.Find("Canvas").GetComponent<UIManager>().SetStar(num);
        if (!isThisWaveOver) {
            return;
        }
        Vector3 lastPos = GameObject.Find("DogCatcherPoint").transform.position;
        for (int i = 0; i < num; i++) {
            GameObject go=(GameObject)Instantiate(dogCatcher,lastPos, Quaternion.identity);
            lastPos = go.transform.position + new Vector3(0, 30, 0);
            hasGeneDogCatcherNum++;
            thisWaveNum = i+1;
            if (hasGeneDogCatcherNum ==maxDogCatcherNum) {
                break;
            }
        }
        isThisWaveOver = false;        
    }
}
