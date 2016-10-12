using UnityEngine;
using System.Collections;

public class DogHouseInventory : MonoBehaviour {

    public GameObject[] items;
    public GameObject InventoryBackground;
    public bool isOpened;

	// Use this for initialization
	void Start ()
    {
        InventoryBackground = GameObject.Find("InventoryBackground");
        isOpened = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButtonDown("Pick") && isOpened == false)
        {
            openInventory();
        }

        if(Input.GetButtonDown("Pick") && isOpened == true)
        {
            closeInventory();
        }
    }

    public void openInventory()
    {
        InventoryBackground.GetComponent<UIvisibility>().showUI();
        isOpened = true;
    }

    public void closeInventory()
    {
        InventoryBackground.GetComponent<UIvisibility>().hideUI();
        isOpened = false;
    }
}
