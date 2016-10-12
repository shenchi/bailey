using UnityEngine;
using System.Collections;

/// <summary>
/// The script should be attached on BaseballBoy and AnotherBaseballBoy
/// </summary>
public class ThrowBaseball : MonoBehaviour {

    public bool hasBall;

    public GameObject otherBoy;

    public GameObject item;

    /// <summary>
    /// The time needed for the boy to be upset when not playing baseball.
    /// </summary>
    public float timeTillUpset = 5.0f;

    /// <summary>
    /// The last time when the boy is playing
    /// </summary>
    public float lastTimePlaying;

    /// <summary>
    /// Is the boy playing?
    /// </summary>
    public bool isPlaying;

    /// <summary>
    /// Is the boy upsetting?
    /// </summary>
    public bool isUpset;

	// Use this for initialization
	void Start ()
    {
	    if(gameObject.name == "BaseballBoy")
        {
            otherBoy = GameObject.Find("OtherBaseballBoy");
        }

        else if(gameObject.name == "OtherBaseballBoy")
        {
            otherBoy = GameObject.Find("BaseballBoy");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(hasBall == true)
        {
            lastTimePlaying = Time.time;
            isPlaying = true;
            throwBall();
        }

        if(hasBall == false && otherBoy.GetComponent<ThrowBaseball>().hasBall == false && Time.time - lastTimePlaying >= timeTillUpset)
        {
            isUpset = true;
        }

        if(item.name != "Baseball")
        {
            item.GetComponent<ItemProperty>().isPickedUp = false;
            item.GetComponent<ItemProperty>().owner = null;
            item = null;
        }

        if(item.name == "Baseball")
        {
            hasBall = true;
        }
	}

    public void throwBall()
    {
        //throw baseball towards otherBoy, probably need a lerp

        //the NPC has a small chance to throw the ball else where
    }
}
