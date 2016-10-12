﻿using UnityEngine;
using System.Collections;

public class NPCProperty : MonoBehaviour {
    
    /// <summary>
    /// The name of this NPC.
    /// </summary>
    public string myName;

    /// <summary>
    /// Is this NPC happy and will take Bailey home?
    /// </summary>
    public bool isHappy;

    /// <summary>
    /// The happiness of this NPC, if it is higher enough, the NPC will be happy, means isHappy will be set to true.
    /// </summary>
    public int NPChappiness;

    /// <summary>
    /// What the happiness this NPC needs to become happy.
    /// </summary>
    public int howMuchToHappy;

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void SetMood(string m) {
        if (m == "bad")
        {
            isHappy = false;
            GetComponent<TextMesh>().text = ":(";
        }
        else if(m=="good"){
            isHappy = true;
            GetComponent<TextMesh>().text = ":)";
        }
    
    }
    public void SetHappniess(int num) {
        NPChappiness = Mathf.Clamp(NPChappiness + num, 0, 100);
    }

}
