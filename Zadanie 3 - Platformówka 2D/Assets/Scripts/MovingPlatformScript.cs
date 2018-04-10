using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {

    public float platformSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.PingPong(Time.time * platformSpeed, 10), transform.position.y, transform.position.z);
    }
}
