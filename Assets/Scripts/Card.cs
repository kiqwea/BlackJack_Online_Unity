using UnityEngine;


public class Card : MonoBehaviour
{
    public enum Suit{
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }


    public int value;
    public Suit suit;
    public bool isAce = false;
    private SpriteRenderer spriteRenderer;

    

    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private int CardCalculate(int cardId){

        int x = cardId;
        while(x >=13){
            x-=13;
        }
        x+=2;
        
        return x;
    }

    public void SetCard(int cardId, Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.LogError($"cardIndex {cardId} returned null sprite!");
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        

        //int x = (cardId + 2) % 13;
        int x = CardCalculate(cardId);
        Debug.Log(x);
        if(x < 11){
            value = x;
        }
        else if (x <= 13){
            value = 10;
        }
        else{
            isAce = true;
        }

        switch (cardId/13){
            case 0:
                suit = Suit.Hearts;
                break;
            case 1:
                suit = Suit.Clubs;
                break;
            case 2:
                suit = Suit.Diamonds;
                break;
            case 3:
                suit = Suit.Spades;
                break;
        }

        //spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = newSprite;
    }
}
