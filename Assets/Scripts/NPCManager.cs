using UnityEngine;
using System.Collections;

public class NPCManager : MonoBehaviour {
    
    public GameObject littleGirl;
    public GameObject baseballBoy;
    public GameObject anotherBaseballBoy;
    public GameObject girlMother;
    public GameObject doll;
    public GameObject baseball;

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
        Instantiate(littleGirl, spawnPoint, spawnRotation);
    }

    public void createGirlMother()
    {
        Vector3 spawnPoint = new Vector3(457, -1265, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        Instantiate(girlMother, spawnPoint, spawnRotation);
    }

    public void createBaseballBoy()
    {
        Vector3 spawnPoint = new Vector3(184, -241, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        Instantiate(baseballBoy, spawnPoint, spawnRotation);
    }

    public void createAnotherBaseballBoy()
    {
        Vector3 spawnPoint = new Vector3(625, -271, -5);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 1);
        Instantiate(anotherBaseballBoy, spawnPoint, spawnRotation);
    }

    public void destroyLittleGirl()
    {
        GameObject toBeDestroy = GameObject.Find("littleGirl");
        Destroy(toBeDestroy);
    }

    public void destroyGirlMother()
    {
        GameObject toBeDestroy = GameObject.Find("girlMother");
        Destroy(toBeDestroy);
    }

    public void destroyBaseballBoy()
    {
        GameObject toBeDestroy = GameObject.Find("BaseballBoy");
        Destroy(toBeDestroy);
    }

    public void destroyAnotherBaseballBoy()
    {
        GameObject toBeDestroy = GameObject.Find("AnotherBaseballBoy");
        Destroy(toBeDestroy);
    }
}
