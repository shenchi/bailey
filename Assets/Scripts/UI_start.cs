using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_start : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public void OnClick()
    {
        
           //     Debug.Log("none");
        SceneManager.LoadScene("main");

    }
    // Update is called once per frame
    void Update () {
	
	}
}
