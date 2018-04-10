using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

    public int maxHealth;
    private int currentHealth;
    private int coins;
    private GameObject tryAgain;

    private static GameStats instance = null;

    // Use this for initialization
    void Start () {
        coins = 0;
        currentHealth = maxHealth;

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
        tryAgain = GameObject.FindWithTag("Finish");
        hidePanel();
    }

    private void OnLevelWasLoaded(int level) {
        hidePanel();
        Debug.Log("Coin status updated");
        GameObject text = GameObject.FindWithTag("Coinscore");
        text.GetComponent<Text>().text = "Coins: " + coins;

        GameObject health = GameObject.FindWithTag("Healthtext");
        Debug.Log("Health status updated");
        health.GetComponent<Text>().text = "Health: " + currentHealth;
    }

    // Update is called once per frame
    void Update () {
    }

    public void CollectCoin() {
        coins++;
        GameObject text = GameObject.FindWithTag("Coinscore");
        text.GetComponent<Text>().text = "Coins: " + coins;
    }

    public void LoseAllCoins() {
        coins = 0;
    }

    public void reduceHealth() {
        currentHealth--;
        if(currentHealth <= 0)
        {
            Debug.Log("You are dead");
            tryAgain.SetActive(true);
            
        }

        Debug.Log("Health status updated");
        GameObject health = GameObject.FindWithTag("Healthtext");
        health.GetComponent<Text>().text = "Health: " + currentHealth;
    }

    public void restart() {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void hidePanel() {
        tryAgain.SetActive(false);
    }
}
