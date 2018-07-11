using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

    public int maxHealth;
    public GameObject tryAgain;

    private int currentHealth;
    private int coins;
    private bool invureable;
    private int invsec;

    private static GameStats instance = null;

    // Use this for initialization
    void Start () {
        coins = 0;
        currentHealth = maxHealth;
        invureable = false;
        invsec = 40;


        GameObject health = GameObject.FindWithTag("Healthtext");
        Debug.Log("Health status updated");
        health.GetComponent<Text>().text = "Health: " + currentHealth;
        

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void OnLevelWasLoaded(int level) {
        invureable = false;
        invsec = 40;
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this) {
            Destroy(this.gameObject);
            return;
        }
        Debug.Log("Coin status updated");
        GameObject text = GameObject.FindWithTag("Coinscore");
        text.GetComponent<Text>().text = "Coins: " + coins;

        GameObject health = GameObject.FindWithTag("Healthtext");
        Debug.Log("Health status updated");
        health.GetComponent<Text>().text = "Health: " + currentHealth;
    }

    // Update is called once per frame
    void Update () {
        if (invureable) {
            if (invsec == 0)
                invureable = false;
            else
                invsec--;

        }

    }

    public void CollectCoin() {
        coins++;
        GameObject text = GameObject.FindWithTag("Coinscore");
        text.GetComponent<Text>().text = "Coins: " + coins;
    }

    public void LoseAllCoins() {
        coins = 0;
        GameObject text = GameObject.FindWithTag("Coinscore");
        text.GetComponent<Text>().text = "Coins: " + coins;
    }

    public void reduceHealth() {
        if (!invureable) {
            currentHealth--;
            invureable = true;
        }

        if(currentHealth <= 0)
        {
            Debug.Log("You are dead");
            var panel = GameObject.Instantiate(tryAgain);
            Canvas cv = GameObject.FindObjectOfType<Canvas>();
            panel.transform.parent = cv.transform;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

            Time.timeScale = 0.0f;
        }

        Debug.Log("Health status updated");
        GameObject health = GameObject.FindWithTag("Healthtext");
        health.GetComponent<Text>().text = "Health: " + currentHealth;
    }


    public void hidePanel() {
    }
}
