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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCard(int cardId, Sprite newSprite)
    {
        Debug.Log("1111");

        int x = (cardId + 2) % 13;
        if(x < 11){
            value = x;
        }
        else if (x < 13){
            value = 10;
        }
        else{
            value = 10;
            isAce = true;
        }
        Debug.Log("22222");

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
        Debug.Log("33333");

        spriteRenderer.sprite = newSprite;
        Debug.Log("4444");
    }
}
