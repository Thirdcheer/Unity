using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    public bool triggered;
	// Use this for initialization
	void Start () {
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            RestartGame();
        }
    }

    public void RestartGame() {
        GameStats gs = GameObject.FindObjectOfType<GameStats>();
        gs.LoseAllCoins();
        Debug.Log("Lost all coins!");
        Application.LoadLevel(Application.loadedLevel);
    }
}
