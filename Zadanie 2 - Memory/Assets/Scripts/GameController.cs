using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.UIElements;

public class GameController : MonoBehaviour
{

    public int gridRows = 4;
    public int gridColums = 4;
    public float offsetRow = 2.5f;
    public float offsetCol = 1.5f;
    
    private int score = 0;
    private int matches = 0;


    public GameObject[] availableCards;
    private GameObject[] cardsToSpawn;
    private Sprite[] image;
    private bool clickingBlocked;

    public Text scoreLabel;
    public Text winLabel;
    public Text totalPairsLabel;
    public Text totalCardsLabel;
    public Text hiscoreLabel;

    private Card firstCard;
    private Card secondCard;
    public GameObject spawnPos;

    string[] playerPrefsKeys = new string[] { "totalPairs", "totalCards", "hiscore" };
    int totalPairs;
    [HideInInspector] public int totalCards;
    int hiscore;

    
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("totalPairs"))
        {
            totalPairs = PlayerPrefs.GetInt("totalPairs");
            totalPairsLabel.text = "Total pairs matched: " + totalPairs.ToString();
        }

        if (PlayerPrefs.HasKey("totalCards"))
        {
            totalCards = PlayerPrefs.GetInt("totalCards");
            totalCardsLabel.text = "Total cards clicked: " + totalCards.ToString();
        }

        if (PlayerPrefs.HasKey("hiscore"))
        {
            hiscore = PlayerPrefs.GetInt("hiscore");
            hiscoreLabel.text = "Highest score: " + hiscore.ToString();
        }

        winLabel.enabled = false;
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
        if(firstCard.cardName == secondCard.cardName)
        {
            totalPairs++;
            totalPairsLabel.text = "Total pairs matched: " + totalPairs.ToString();
            score += 10;
            scoreLabel.text = "Score: " + score;
            matches += 1;

            Debug.Log("Matches " + matches);

            if (matches == 8)
            {
                Debug.Log("Matches completed" + matches);
                winLabel.enabled = true;
                if(score > hiscore)
                {
                    PlayerPrefs.SetInt("hiscore", score);
                }
                PlayerPrefs.SetInt("totalPairs", totalPairs);
                PlayerPrefs.SetInt("totalCards", totalCards);
            }
        }
        else
        {
            yield return new WaitForSeconds(1.0f);

            score -= 2;
            scoreLabel.text = "Score: " + score;

            clickingBlocked = true;
            firstCard.ChangeState(new UnRevealedState(this));
            secondCard.ChangeState(new UnRevealedState(this));
            clickingBlocked = false;
        }

        firstCard = null;
        secondCard = null;
    }

    public bool Blocked()
    {
        return clickingBlocked;
    }

    public bool canBeRevealed()
    {
        return secondCard == null;
    }

    public bool twoRevealed()
    {
        return firstCard != null && secondCard != null;
    }

    public void UpdateStats()
    {
        totalCards++;
        totalCardsLabel.text = "Total cards clicked: " + totalCards.ToString();
    }
}


