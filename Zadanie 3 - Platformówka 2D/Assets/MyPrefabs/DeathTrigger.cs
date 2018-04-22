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
            RestartLevel(other.gameObject);
        }
    }

    public void RestartLevel(GameObject player) {
        GameStats gs = GameObject.FindObjectOfType<GameStats>();
        gs.LoseAllCoins();
        Debug.Log("Lost all coins!");
        player.transform.position = new Vector2(0, 0);
        Time.timeScale = 1.0f;
    }

    public void RestartGame() {
        GameObject gs = GameObject.FindGameObjectWithTag("GameController");
        Destroy(gs);
        Application.LoadLevel(0);
        Time.timeScale = 1.0f;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
