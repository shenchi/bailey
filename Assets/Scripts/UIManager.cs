using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public GameObject StarPanel;
    public GameObject[] Stars;
    private float flashTime;
    private bool isFlashing;
	// Use this for initialization
	void Start () {
        flashTime = 1;
	}
	
	// Update is called once per frame
	void Update () {
        StarFlash();
    }
    public void SetStar(int i) {
        foreach (GameObject star in Stars) {
            star.SetActive(false);
        }
        for (int j = 0; j < i; j++) {
            Stars[j].SetActive(true);
        }   
    }
    public void StarFlashOn() {
        isFlashing = true;
    }
    public void StarFlashOff() {
        isFlashing = false;
        StarPanel.SetActive(true);
    }
    void StarFlash()
    {
        if (isFlashing)
        {
            if (flashTime == 1)
            {
                StarPanel.SetActive(!StarPanel.activeInHierarchy);
                flashTime -= Time.deltaTime;
            }
            else
            {
                flashTime -= Time.deltaTime;
                if (flashTime <= 0)
                {
                    flashTime = 1;
                }
            }
        }
    }
}
