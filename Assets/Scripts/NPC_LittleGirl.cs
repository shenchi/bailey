using UnityEngine;
using System.Collections;

public class NPC_LittleGirl : MonoBehaviour {
    public GameEventManager GEM;
    public int EventIndex;
    private float EventTime;

	// Use this for initialization
	void Start () {
        GEM = GameObject.Find("GameController").GetComponent<GameEventManager>();
        EventIndex = 0;
        EventTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad >= 3&&EventIndex==0) {
            GEM.StartEvent(0, EventIndex++, gameObject);
            EventTime = Time.timeSinceLevelLoad;
        }
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 50&&EventIndex==1) {
            GEM.StartEvent(0, EventIndex++, gameObject);
            EventTime = Time.timeSinceLevelLoad;
        }
        
        if (Time.timeSinceLevelLoad-EventTime>=5 && EventIndex == 2) {
            GEM.StartEvent(0, EventIndex++, gameObject);
            EventTime = Time.timeSinceLevelLoad;
        }
        if (Time.timeSinceLevelLoad - EventTime >= 5 && EventIndex == 3)
        {
            GEM.StartEvent(0, EventIndex++, gameObject);
        }
    }
    
}
