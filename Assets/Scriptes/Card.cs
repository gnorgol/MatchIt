using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;
    public Image cardImage;
    public Sprite frontSprite;
    public Sprite backSprite;


    private bool isFlipped = false;

    public void SetupCard(int id, Sprite front)
    {
        cardID = id;
        frontSprite = front;
        cardImage.sprite = backSprite;
        isFlipped = false;
    }

    public void FlipCard()
    {
        if (isFlipped) return;

        Debug.Log("NumCardsFlip: " + GameManager.Instance.NumCardsFlip);
        if (GameManager.Instance.NumCardsFlip >= 2)
        {
            return;
        }
        GameManager.Instance.NumCardsFlip++;
        


        isFlipped = true;
        cardImage.sprite = frontSprite;
        GameManager.Instance.CardFlipped(this);
    }

    public void HideCard()
    {
        isFlipped = false;
        cardImage.sprite = backSprite;
    }
}
