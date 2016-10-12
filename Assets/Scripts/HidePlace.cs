using UnityEngine;
using System.Collections;

public class HidePlace : MonoBehaviour {
    public GameObject Player;
    public float crimeIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Player != null) {
            crimeIndex = Mathf.Clamp(crimeIndex - Time.deltaTime, 0, 100);
            Player.GetComponent<PlayerController>().CrimeIndex = Mathf.FloorToInt(crimeIndex);
            if (Input.GetButtonDown("GoOut"))
            {               
                Player.SetActive(true);
                Player = null;
            }
        }
        
	}
    public void HidePlayer(GameObject p) {
        Player = p;
        crimeIndex = p.GetComponent<PlayerController>().CrimeIndex;
        p.SetActive(false);
    }
}
