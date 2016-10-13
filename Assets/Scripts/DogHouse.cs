using UnityEngine;
using System.Collections;

public class DogHouse : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            if (other.GetComponent<ItemProperty>().owner.tag == "Player")
            {
                GameObject playerDog = GameObject.FindGameObjectWithTag("Player");
                playerDog.GetComponentInChildren<TextMesh>().text = "Press E to store the item in dog house";
            }

            other.GetComponent<ItemProperty>().isHome = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            other.GetComponent<ItemProperty>().isHome = false;

            if (other.GetComponent<ItemProperty>().owner.tag == "Player")
            {
                GameObject playerDog = GameObject.FindGameObjectWithTag("Player");
                playerDog.GetComponentInChildren<TextMesh>().text = "";
            }
        }
    }
}
