using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

    [HideInInspector] public int points;
    public int enemiesLeft;

    public Text pointsText;
    public Text enemiesText;

    
	// Use this for initialization
	void Start () {
        points = 0;
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        pointsText.text = points.ToString();
        enemiesText.text = enemiesLeft.ToString();

        Messenger.AddListener("point earned", AddPoint);
    }
	
    public void AddPoint() {
        points++;
        enemiesLeft--;
        pointsText.text = points.ToString();
        enemiesText.text = enemiesLeft.ToString();
        Debug.Log("Points: " + points + ", Enemies left: " + enemiesLeft);
    }
}
