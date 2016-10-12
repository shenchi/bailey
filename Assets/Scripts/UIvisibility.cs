using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIvisibility : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    /// <summary>
    /// Show this current UI element
    /// </summary>
    public void showUI()
    {
        gameObject.GetComponent<Image>().enabled = true;
    }

    /// <summary>
    /// Hide this current UI element
    /// </summary>
    public void hideUI()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }
}
