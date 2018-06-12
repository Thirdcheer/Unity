using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public GameObject cardBack;
    private GameController gameController;
    [HideInInspector] public IState currentState;

    public string cardName;

	// Use this for initialization
	void Start () {
        
        Debug.Log(GameObject.FindGameObjectsWithTag("GameController").Length);
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();

        currentState = new UnRevealedState(gameController);
        currentState.Start(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        Debug.Log(currentState);
        currentState.OnMouseDown(this);
    }

    public void ChangeState(IState state)
    {
        currentState = state;
        currentState.Start(this);
        //cardBack.SetActive(true);
    }


}
