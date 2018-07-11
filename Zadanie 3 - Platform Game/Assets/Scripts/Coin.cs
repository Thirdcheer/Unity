using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private Vector3 startPos;
    GameStats gs;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        gs = GameObject.FindObjectOfType<GameStats>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3( transform.position.x, startPos.y + Mathf.PingPong(Time.time * 0.5f, 0.25f), transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Coin collected");
            Destroy(gameObject);
            gs.CollectCoin();
        }
    }
}
