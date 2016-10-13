using UnityEngine;
using System.Collections;

public class NPCManager : MonoBehaviour {
    
    public GameObject littleGirl;
    public GameObject baseballBoy;
    public GameObject anotherBaseballBoy;
    public GameObject girlMother;
    public GameObject doll;
    public GameObject baseball;

    public GameObject currentLittleGirl;
    public GameObject currentBaseballBoy;
    public GameObject currentAnotherBaseballBoy;
    public GameObject currentGirlMother;
    public GameObject currentDoll;
    public GameObject currentBaseball;

    public GameObject timeManager;

    // Use this for initialization
    void Start ()
    {
        timeManager = GameObject.Find("TimeManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void createLittleGirl()
    {
        Vector3 spawnPoint = new Vector3(479, -1271, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        currentLittleGirl = (GameObject)Instantiate(littleGirl, spawnPoint, spawnRotation);
        currentLittleGirl.GetComponent<PathFinder>().FindPath(809, -474);
        createGirlMother();
    }

    public void destroyLittleGirl()
    {
        GameObject toBeDestroy = GameObject.Find("LittleGirl");
        destroyGirlMother();
        Destroy(toBeDestroy);
    }

    public void createGirlMother()
    {
        Vector3 spawnPoint = new Vector3(457, -1265, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        currentGirlMother = (GameObject)Instantiate(girlMother, currentLittleGirl.transform);
        //currentGirlMother.transform.parent = currentLittleGirl.transform;
    }

    public void destroyGirlMother()
    {
        GameObject toBeDestroy = GameObject.Find("girlMother");
        Destroy(toBeDestroy);
    }

    public void createBaseballBoy()
    {
        Vector3 spawnPoint = new Vector3(184, -241, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        currentBaseballBoy = (GameObject)Instantiate(baseballBoy, spawnPoint, spawnRotation);
        currentBaseballBoy.GetComponent<PathFinder>().FindPath(515, -922);
    }

    public void destroyBaseballBoy()
    {
        GameObject toBeDestroy = GameObject.Find("BaseballBoy");
        Destroy(toBeDestroy);
    }

    public void createAnotherBaseballBoy()
    {
        Vector3 spawnPoint = new Vector3(625, -271, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        currentAnotherBaseballBoy = (GameObject)Instantiate(anotherBaseballBoy, spawnPoint, spawnRotation);
        currentAnotherBaseballBoy.GetComponent<PathFinder>().FindPath(511, -660);
    }

    public void destroyAnotherBaseballBoy()
    {
        GameObject toBeDestroy = GameObject.Find("AnotherBaseballBoy");
        Destroy(toBeDestroy);
    }

    public void createBaseball()
    {
        Vector3 spawnPoint = new Vector3(625, -271, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        currentBaseball = (GameObject)Instantiate(baseball);
        currentBaseball.GetComponent<ItemProperty>().isPickedUp = true;
        currentBaseball.GetComponent<ItemProperty>().owner = currentBaseballBoy;
        currentBaseballBoy.GetComponent<ThrowBaseball>().hasBall = true;
        currentBaseballBoy.GetComponent<ThrowBaseball>().isAtPlayingPosition = false;
    }

    public void destroyBaseball()
    {
        GameObject toBeDestroy = GameObject.Find("Baseball");
        Destroy(toBeDestroy);
    }

    public void createDoll()
    {
        Vector3 spawnPoint = new Vector3(479, -1271, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        currentDoll = (GameObject)Instantiate(doll);
        currentDoll.GetComponent<ItemProperty>().isPickedUp = true;
        currentDoll.GetComponent<ItemProperty>().owner = currentLittleGirl;
    }

    public void destroyDoll()
    {
        GameObject toBeDestroy = GameObject.Find("Doll");
        Destroy(toBeDestroy);
    }
}
