using UnityEngine;
using System.Collections;

public interface IState 
{
    void Start(Card card);
    void OnMouseDown(Card card);
}

public class RevealedState :  IState
{
    GameController gameController;


    public RevealedState(GameController gameController)
    {
        this.gameController = gameController;
    }

    public void OnMouseDown(Card card)
    {
        card.cardBack.SetActive(false);
        gameController.CardRevealed(card);
        gameController.UpdateStats();
    }

    public void Start(Card card)
    {
        card.cardBack.SetActive(true);
    }

}

public class UnRevealedState :  IState
{
    GameController gameController;

    public UnRevealedState(GameController gameController)
    {
        this.gameController = gameController;

    }
    public void OnMouseDown(Card card)
    {
        if (gameController.canBeRevealed() && !gameController.Blocked())
        {
            card.cardBack.SetActive(false);
            gameController.CardRevealed(card);
            card.currentState = new RevealedState(gameController);
            gameController.UpdateStats();
        }
    }

    public void Start(Card card)
    {
        if(!card.cardBack.active)
            card.cardBack.SetActive(true);
    }

}