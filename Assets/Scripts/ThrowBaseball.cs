using UnityEngine;
using System.Collections;

/// <summary>
/// The script should be attached on BaseballBoy and AnotherBaseballBoy
/// </summary>
public class ThrowBaseball : MonoBehaviour {

    public bool hasBall;
    public bool isAtPlayingPosition;

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

    private bool isArrived;
	// Use this for initialization
	void Start ()
    {

	}

    // Update is called once per frame
    void Update()
    {
        if (isArrived)
        {
            if (!isUpset&&hasBall == false && otherBoy.GetComponent<ThrowBaseball>().hasBall == false && (Time.time - lastTimePlaying) >= timeTillUpset)
            {
                isUpset = true;
                GetComponent<NPCProperty>().SetMood("bad");
            }
            if (null == item)
            {
                hasBall = false;
                return;
            }

            if (hasBall == true)
            {
                lastTimePlaying = Time.time;
                isPlaying = true;
                throwBall();
            }

            

            //if (item.name != "Baseball")
            //{
            //    item.GetComponent<ItemProperty>().isPickedUp = false;
            //    item.GetComponent<ItemProperty>().owner = null;
            //    item = null;
            //}

            //if (item.name == "Baseball")
            //{
            //    hasBall = true;
            //}
        }
    }

    public void throwBall()
    {
        item.GetComponent<ItemProperty>().Drop(gameObject);
        //throw baseball towards otherBoy, probably need a lerp
        float randomNum = Random.Range(0f, 1f);

        if (randomNum >= 0 && randomNum <= 0.95f)
        {
            item.GetComponent<LinearMovement>().messageReceiver = otherBoy;
            item.GetComponent<LinearMovement>().MoveTo(otherBoy);
        }
        //the NPC has a small chance to throw the ball else where

        else if (randomNum > 0.95f && randomNum <= 1)
        {
            item.GetComponent<LinearMovement>().MoveTo(new Vector3(otherBoy.transform.position.x + 60, otherBoy.transform.position.y, otherBoy.transform.position.z));
            item.GetComponent<LinearMovement>().messageReceiver = null;
        }

        item = null;
        hasBall = false;
    }

    public void OnBallCatched(GameObject ball)
    {
        lastTimePlaying = Time.time;
        hasBall = true;
        item = ball;
    }
    public void Arrived() {
        isArrived = true;
        lastTimePlaying = Time.time;
    }
}
