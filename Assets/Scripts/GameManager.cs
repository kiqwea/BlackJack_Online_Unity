using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform dealerHand;
    public TextMeshProUGUI playerScoreText;

    private Sprite[] cardSprites;
    private List<int> usedCards = new List<int>();
    private System.Random random = new System.Random();
    private List<Card> playerCards = new List<Card>();
    private List<Card> dealerCards = new List<Card>();

    public Sprite cardsSprite;

    void Start()
    {

        cardSprites = Resources.LoadAll<Sprite>("Sprites/cardatlas"); 

        Debug.Log("lenght: " + cardSprites.Length);

        DealCards();
    }

    void DealCards()
    {
   
        for (int i = 0; i < 2; i++)
        {
            int cardIndex;
            do
            {
                cardIndex = random.Next(0, 52);
            } while (usedCards.Contains(cardIndex));
            usedCards.Add(cardIndex);


            GameObject newCard = Instantiate(cardPrefab, playerHand);
            newCard.transform.position = new Vector3(-2 + i * 1.5f, -2, 0); 
            Card card = newCard.GetComponent<Card>();

            Debug.Log(cardSprites[cardIndex].name);

            card.SetCard(cardIndex, cardSprites[cardIndex]);
            playerCards.Add(card);
        }

        for (int i = 0; i < 2; i++)
        {
            int cardIndex;
            do
            {
                cardIndex = random.Next(0, cardSprites.Length);
            } while (usedCards.Contains(cardIndex));
            usedCards.Add(cardIndex);

            GameObject newCard = Instantiate(cardPrefab, dealerHand);
            newCard.transform.position = new Vector3(-2 + i * 1.5f, 2, 0);
            Card card = newCard.GetComponent<Card>();

            
            card.SetCard(cardIndex, cardSprites[cardIndex]);
            dealerCards.Add(card); 
        }

        Debug.Log(playerCards.Count + " -- " + dealerCards.Count);

        int playerScore = CalculateScore(playerCards);
        playerScoreText.text = "Score: " + playerScore;
    }

    private int CalculateScore(List<Card> cards)
    {
        int score = 0;
        foreach (Card card in cards)
        {
            if (card != null)
            {
                if (card.isAce && score + 11 <= 21) 
                    score += 11;
                else if (card.isAce)
                    score += 1;
                else
                    score += card.value;
            }
        }
        return score;
    }
}