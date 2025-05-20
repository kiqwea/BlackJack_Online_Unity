using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject suitCardPrefab;
    public Transform playerHand;
    public Transform dealerHand;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI dealerScoreText;
    public UnityEngine.UI.Button newGameButton;
    public UnityEngine.UI.Button betButton;

    public UnityEngine.UI.Slider betSlider;
    private List<GameObject> allCards = new List<GameObject>();


    private Sprite[] cardSprites;
    private List<int> usedCards = new List<int>();
    private System.Random random = new System.Random();

    Dealer dealer = new Dealer();
    Player player = new Player();


    public Sprite cardsSprite;
    public Sprite suitSprite;

    private int bet;
    private enum resoult{
        win,
        loose,
        push
    }

    void Start()
    {
        newGameButton.gameObject.SetActive(false);
        cardSprites = Resources.LoadAll<Sprite>("Sprites/cardatlas");

        NewGame();
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
        dealerScoreText.gameObject.SetActive(false);


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

            Card card = newCard.GetComponent<Card>();
            card.SetCard(cardIndex, cardSprites[cardIndex], suitSprite);
            dealer.cards.Add(card);
            dealer.CalculateScore();

            if(dealer.cards.Count == 2)
                card.ShowBack();
            
            
        }
        allCards.Add(newCard);
        

    }
    public void HitBittonClick()
    {
        if(player.score < 21){
            TakeNewcard(true);
            playerScoreText.text = "Your score: " + player.score;
        }

        if(player.score > 21){
            ResolveOutcome(resoult.loose);
        }
        else if(player.score == 21){
            ResolveOutcome(resoult.win);
        }
        
    }

    public void DoubleButtonClick(){

    }
    public void SplitButtonClick(){
        if(player.cards.Count == 2 && player.cards[0].rank == player.cards[1].rank){
            
        }
    }

    private resoult ResoultCheck(){
        if(player.score > dealer.score || dealer.score > 21){
            return resoult.win;
        }
        else if(player.score == dealer.score){
            return resoult.push;
        }
        
        return resoult.loose;
    }

    private void ResolveOutcome(resoult resoult){
        if(bet > 0){
            if(resoult == resoult.win){
                player.balance += bet;
                
                bet = 0;
            }
            else if(resoult == resoult.loose){
                player.balance =- bet;
            }
        }
        player.SetBalance();
        newGameButton.gameObject.SetActive(true);
    }



    public void StandButton()
    {
        while (dealer.score < 17)
        {
            TakeNewcard(false);
            dealer.CalculateScore();
        }
        dealer.cards[1].ShowFace();

        dealerScoreText.gameObject.SetActive(true);
        dealerScoreText.text = "Dealer score:" + dealer.score;

        ResolveOutcome(ResoultCheck());
        
    }

    public void NewGame(){
        betButton.GetComponentInChildren<TextMeshProUGUI>().text = ((int)betSlider.value).ToString();

        newGameButton.gameObject.SetActive(false);
        SetSlider();

        for (int i = 0; i < allCards.Count; i++)
        {
            Destroy(allCards[i]);
        }
        usedCards.Clear();
        dealer.cards.Clear();
        player.cards.Clear();
        
        DealCards();
    }

    private void SetSlider(){
        betSlider.maxValue = player.balance;
    }
    public void SliderMoove(){
        betButton.GetComponentInChildren<TextMeshProUGUI>().text = ((int)betSlider.value).ToString();
    }

    public void MakeBet(){
        bet = (int)betSlider.value;
    }


}