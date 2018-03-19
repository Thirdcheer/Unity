using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int gridRows = 4;
    public int gridColums = 4;
    public float offsetRow = 2.5f;
    public float offsetCol = 1.5f;
    
    private int score = 0;

    public GameObject[] availableCards;
    private GameObject[] cardsToSpawn;
    private Sprite[] image;

    public Text scoreLabel;

    private Card firstCard;
    private Card secondCard;
    private GameObject spawnPos;



    // Use this for initialization
    void Start()
    {
        Vector3 startPos = spawnPos.transform.position;

        cardsToSpawn = new GameObject[16];
        int cardToSpawn = 0;

        for (int x = 0; x < availableCards.Length; x++)
        {
            cardsToSpawn[x] = availableCards[x];
            cardsToSpawn[availableCards.Length + x] = availableCards[x];
            
        }

        cardsToSpawn = cardsToSpawn.OrderBy(x => Random.Range(2, 16)).ToArray();
       
        for(int i = 0; i<gridColums; i++)
        {
            for(int j=0; j<gridRows; j++)
            {
                GameObject card;
                card = Instantiate(cardsToSpawn[cardToSpawn]);

                cardToSpawn++;

                float posX = (offsetCol * i) + startPos.x; 
                float posY = (offsetRow * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CardRevealed(Card card)
    {
        if(firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    public IEnumerator CheckMatch()
    {
        if(firstCard.name == secondCard.name)
        {
            score += 10;
            scoreLabel.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstCard.Unreveal();
            secondCard.Unreveal();
        }

        firstCard = null;
        secondCard = null;
    }

    public bool canBeRevealed()
    {
        return secondCard == null;
    }
}


