using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject suitCardPrefab;
    public Transform playerHand;
    public Transform dealerHand;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI dealerScoreText;


    private Sprite[] cardSprites;
    private List<int> usedCards = new List<int>();
    private System.Random random = new System.Random();

    Dealer dealer = new Dealer();
    Player player = new Player();


    public Sprite cardsSprite;
    public Sprite suitSprite;

    void Start()
    {
        cardSprites = Resources.LoadAll<Sprite>("Sprites/cardatlas");

        DealCards();
        //SpawnSuit();
    }

    void DealCards()
    {
        for (int i = 0; i < 2; i++)
        {
            TakeNewcard(true);
        }


        for (int i = 0; i < 2; i++)
        {
            TakeNewcard(false);
        }


        player.CalculateScore();
        dealer.CalculateScore();
        playerScoreText.text = "Your score: " + player.score;
        dealerScoreText.text = "Dealer score:" + dealer.score;

    }

    void TakeNewcard(bool isPayer)
    {
        int cardIndex;
        do
        {
            cardIndex = random.Next(0, 52);
        } while (usedCards.Contains(cardIndex));
        usedCards.Add(cardIndex);


        GameObject newCard = Instantiate(cardPrefab, playerHand);
        if (isPayer)
        {
            newCard.transform.position = new Vector3(-2 + player.cards.Count * 1.1f, -2, -1);
            Card card = newCard.GetComponent<Card>();
            card.SetCard(cardIndex, cardSprites[cardIndex], suitSprite);
            player.cards.Add(card);
            player.CalculateScore();
            
        }
        else
        {

            newCard.transform.position = new Vector3(-2 + dealer.cards.Count * 1.1f, 2, -1);
            if(dealer.cards.Count == 1)
                newCard.transform.localScale = new Vector3(0.16f,0.16f,0);

            Card card = newCard.GetComponent<Card>();
            card.SetCard(cardIndex, cardSprites[cardIndex], suitSprite);
            dealer.cards.Add(card);
            dealer.CalculateScore();

            if(dealer.cards.Count == 2)
                card.ShowBack();
            
            
        }
        

    }
    public void HitBittonClick()
    {
        TakeNewcard(true);

        playerScoreText.text = "Your score: " + player.score;
    }

    public void StandButton()
    {
        while (dealer.score < 17)
        {
            dealer.cards[1].ShowFace();
            TakeNewcard(false);
            dealer.CalculateScore();
        }

        dealerScoreText.text = "Dealer score:" + dealer.score;
    }

    // void SpawnSuit()
    // {
    //     Vector3 spawnPosition = new Vector3(-1.5f, 0.7f, -2);
    //     GameObject cardSuit = Instantiate(suitCardPrefab, spawnPosition, Quaternion.identity);
        
    // }




}