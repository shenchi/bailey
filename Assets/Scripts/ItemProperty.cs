using UnityEngine;
using System.Collections;

public class ItemProperty : MonoBehaviour {

    /// <summary>
    /// Is this item being picked up by the player?
    /// </summary>
    public bool isPickedUp;

    /// <summary>
    /// The current owner of this item.
    /// </summary>
    public GameObject owner;

    /// <summary>
    /// The correct NPC this item is used to iteract with.
    /// </summary>
    public string correctNPC;

    /// <summary>
    /// How much happiness the NPC will gain if this item is handed to the correct NPC.
    /// </summary>
    public int happiness;

	// Use this for initialization
	void Start ()
    {
        isPickedUp = false;
        owner = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /// <summary>
        /// If the item is picked up by someone, the transform will follow the owner.
        /// </summary>
        if (isPickedUp == true)
        {
            gameObject.transform.position = owner.transform.position;
            gameObject.transform.rotation = owner.transform.rotation;
        }

        if (owner.GetComponent<NPCProperty>().myName == correctNPC)
        {
            owner.GetComponent<NPCProperty>().NPChappiness += happiness;
            Destroy(gameObject);
        }
	}
}
