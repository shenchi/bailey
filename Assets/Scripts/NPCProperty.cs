using UnityEngine;
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
    public int NPCHappiness;

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
            GetComponentInChildren<TextMesh>().text = ":(";
        }
        else if(m=="good"){
            isHappy = true;
            GetComponentInChildren<TextMesh>().text = ":)";
        }
    
    }
    public void SetHappniess(int num) {
        NPCHappiness = Mathf.Clamp(NPCHappiness + num, 0, 100);
    }

}
