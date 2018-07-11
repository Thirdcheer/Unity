using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {

    public float platformSpeed;
    public float length;
    public int direction = 1;

	// Use this for initialization
	void Start () {
        if (direction == 0)
            direction = 1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(direction * Mathf.PingPong(Time.time * platformSpeed, length), transform.position.y, transform.position.z);
    }
}
