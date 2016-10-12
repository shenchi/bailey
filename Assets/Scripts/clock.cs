using UnityEngine;
using System.Collections;
using System;
public class clock : MonoBehaviour {
    private const float secondsToDegrees = 360f / 60f;
    public Transform seconds;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        DateTime time = DateTime.Now;
        seconds.localRotation = Quaternion.Euler(0f, 0f, time.Second * -secondsToDegrees);
    }
}
