using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	float lifespan = 3.0f;
	public GameObject fireEffect;
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;
		
		if(lifespan <= 0) {
			Explode();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		
		if(collision.gameObject.tag == "Enemy") {
            Debug.Log("Don't shoot!");
            //GameObject gc = GameObject.Find("GameController");
            // gc.GetComponent<GameController>().AddPoint();
            Messenger.Broadcast("point earned");
            Instantiate(fireEffect, collision.transform.position, new Quaternion(0,0,0,0));
			Destroy(gameObject);
            Destroy(collision.gameObject);
		}
	}
	
	void Explode() {
		Destroy(gameObject);
	}
}
