using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public GameObject cardBack;
    private GameController gameController;

    public string cardName;

	// Use this for initialization
	void Start () {
        if (!cardBack.active)
        {

            cardBack.SetActive(true);
        }

        Debug.Log(GameObject.FindGameObjectsWithTag("GameController").Length);

        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && gameController.canBeRevealed() && !gameController.Blocked())
        {
            cardBack.SetActive(false);
            gameController.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }


}
