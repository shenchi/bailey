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
    public int NPCHappiness;

    /// <summary>
    /// What the happiness this NPC needs to become happy.
    /// </summary>
    public int howMuchToHappy;

    public int hp;
    private float RedTime;

    // Use this for initialization
    void Start ()
    {
        RedTime = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (RedTime >= 0)
        {
            RedTime -= Time.deltaTime;
            if (RedTime <= 0)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
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
    public void GetCorrectItem() {
        print("Thank you!");
    }
    public void TakeAttack(int damage) {
        // shader turn red 
        GetComponent<SpriteRenderer>().color = Color.red;
        RedTime = 0.3f;
        hp = Mathf.Clamp(hp - damage, 0, hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
