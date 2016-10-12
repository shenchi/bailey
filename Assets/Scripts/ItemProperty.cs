using UnityEngine;
using System.Collections;

public class ItemProperty : MonoBehaviour {

    public bool isPickedUp;
    public GameObject owner;

	// Use this for initialization
	void Start ()
    {
        isPickedUp = false;
        owner = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(isPickedUp = true)
        {
            gameObject.transform.position = owner.transform.position;
            gameObject.transform.rotation = owner.transform.rotation;
        }
	}
}
