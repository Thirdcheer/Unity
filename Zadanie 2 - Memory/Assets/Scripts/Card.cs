using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    GameObject cardBack;
    public GameController gameController;

    public string name;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && gameController.canBeRevealed())
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
