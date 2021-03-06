﻿using UnityEngine;
using System.Collections;

public class ItemProperty : MonoBehaviour {

    /// <summary>
    /// Is this item being picked up by the player?
    /// </summary>
    public bool isPickedUp = false;

    /// <summary>
    /// The current owner of this item.
    /// </summary>
    public GameObject owner = null;

    /// <summary>
    /// The correct NPC this item is used to iteract with.
    /// </summary>
    public string correctNPC;

    /// <summary>
    /// How much happiness the NPC will gain if this item is handed to the correct NPC.
    /// </summary>
    public int happiness;

    /// <summary>
    /// Is this item placed in dog house?
    /// </summary>
    public bool isHome = false;

    public GameObject previousOwner;

	// Use this for initialization
	void Start ()
    {
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
	}
    public void Drop(GameObject preOwner) {
        isPickedUp = false;
        previousOwner = preOwner;
        owner = null;
    }
    public void PickUp(GameObject Owner) {
        isPickedUp = true;
        owner = Owner;
    }
    public bool Return(GameObject other) {
        NPCProperty NP = other.GetComponent<NPCProperty>();
        if (NP != null && NP.myName == correctNPC)
        {//return successful
            NP.SetHappniess(happiness);
            NP.GetCorrectItem();
            isPickedUp = true;
            previousOwner = owner;
            owner = other;
            return (true);
        }
        else {
            return (false);
        }
    }
}
