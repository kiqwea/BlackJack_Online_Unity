using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


public class Player
{
    public int score;
    public int balance = 1000;
    public List<Card> cards = new List<Card>();

    public Player(){
        score = GetBalance();
    }

    private int GetBalance(){ //from bd
        return 1000;
    }

    public void SetBalance(){ //balance to bd

    }

    public void CalculateScore()
    {
        score = 0;
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
                    
                    int index = cards.FindIndex(x => x.value == 11);
                    Card card = cards[index];
                    card.value = 1;
                    cards[index] = card;
                    score -= 10;
                }
            }
                    
        }
    }

}
