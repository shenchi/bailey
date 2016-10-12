using UnityEngine;
using System.Collections;

public class HidePlace : MonoBehaviour {
    public GameObject Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Player != null) {
            if (Input.GetButtonDown("GoOut"))
            {               
                Player.SetActive(true);
                Player = null;
            }
        }
        
	}
    public void HidePlayer(GameObject p) {
        Player = p;
        p.SetActive(false);
    }
}
