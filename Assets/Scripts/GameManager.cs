using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using System.Linq;

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
            // int cardIndex;
            // do
            // {
            //     cardIndex = random.Next(0, 52);
            // } while (usedCards.Contains(cardIndex));
            // usedCards.Add(cardIndex);


            // GameObject newCard = Instantiate(cardPrefab, playerHand);
            // newCard.transform.position = new Vector3(-2 + playerCards.Count * 1.3f, -2, -1); 
            // Card card = newCard.GetComponent<Card>();


            // Debug.Log(cardSprites[cardIndex].name);

            // card.SetCard(cardIndex, cardSprites[cardIndex]);
            // playerCards.Add(card);
            TakeNewcard(true);
        }
        

        for (int i = 0; i < 2; i++)
        {
            TakeNewcard(false);
            // int cardIndex;
            // do
            // {
            //     cardIndex = random.Next(0, 52);
            // } while (usedCards.Contains(cardIndex));
            // usedCards.Add(cardIndex);

            // GameObject newCard = Instantiate(cardPrefab, dealerHand);
            // newCard.transform.position = new Vector3(-2 + dealerCards.Count * 1.3f, 2, -1);
            // Card card = newCard.GetComponent<Card>();

            
            // card.SetCard(cardIndex, cardSprites[cardIndex]);
            // dealerCards.Add(card); 
        }

        Debug.Log(playerCards[0].value + " -- " + playerCards[1].value);

        int playerScore = CalculateScore(ref playerCards);
        playerScoreText.text = "Score: " + playerScore;
    }

    void TakeNewcard(bool isPayer){
        int cardIndex;
            do
            {
                cardIndex = random.Next(0, 52);
            } while (usedCards.Contains(cardIndex));
            usedCards.Add(cardIndex);


            GameObject newCard = Instantiate(cardPrefab, playerHand);
            if(isPayer){
                newCard.transform.position = new Vector3(-2 + playerCards.Count * 1.3f, -2, -1); 
            }else{
                newCard.transform.position = new Vector3(-2 + dealerCards.Count * 1.3f, 2, -1); 
            }
            Card card = newCard.GetComponent<Card>();

            Debug.Log(cardSprites[cardIndex].name);

            card.SetCard(cardIndex, cardSprites[cardIndex]);
            if(isPayer){
                playerCards.Add(card);
            }else{
                dealerCards.Add(card);
            }
    }
    public void HitBittonClick(){
        int cardIndex;
            do
            {
                cardIndex = random.Next(0, 52);
            } while (usedCards.Contains(cardIndex));
            usedCards.Add(cardIndex);


            GameObject newCard = Instantiate(cardPrefab, playerHand);
            newCard.transform.position = new Vector3(-2 + playerCards.Count * 1.3f, -2, -1); 
            Card card = newCard.GetComponent<Card>();

            Debug.Log(cardSprites[cardIndex].name);

            card.SetCard(cardIndex, cardSprites[cardIndex]);
            playerCards.Add(card);

            playerScoreText.text = "Score: " + CalculateScore(ref playerCards);
            //CalculateScore(ref playerCards);
    }

    private int CalculateScore(ref List<Card> cards)
    {
        int score = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].isAce && score + 11 <= 21){
                score += 11;
                cards[i].value = 11;
            } 
            else if (cards[i].isAce){
                score += 1;
                cards[i].value = 1;
            }
            else{
                score += cards[i].value;
                if(score > 21 && cards.Any(x => x.value == 11)){
                    cards[cards.FindIndex(x => x.value == 11)].value = 1;
                }
            }
                    
        }
        // foreach (Card card in cards)
        // {
        //     if (card != null)
        //     {
        //         if (card.isAce && score + 11 <= 21) 
        //             score += 11;
        //         else if (card.isAce)
        //             score += 1;
        //         else
        //             score += card.value;
        //     }
        // }
        return score;
    }
}