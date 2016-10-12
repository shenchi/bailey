using UnityEngine;
using System.Collections;

public class NPC_LittleGirl : MonoBehaviour {
    public GameEventManager GEM;
    public int EventIndex;

	// Use this for initialization
	void Start () {
        GEM = GameObject.Find("GameController").GetComponent<GameEventManager>();
        EventIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad >= 10&&EventIndex==0) {
            GEM.StartEvent(0, EventIndex++, gameObject);
        }
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 10&&EventIndex==1) {
            GEM.StartEvent(0, EventIndex++, gameObject);
        }
        if (transform.position.x == 1288 && transform.position.y == -267 && EventIndex == 2) {
            GEM.StartEvent(0, EventIndex++, gameObject);
        }	
	}
    
}
