using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform cardGrid;
    public GameObject cardPrefab;
    public Sprite[] cardFrontSprites;

    private List<Card> allCards = new List<Card>();
    private Card firstFlippedCard;
    private Card secondFlippedCard;

    private int numCardsFlip = 0;
    public int NumCardsFlip
    {
        get { return numCardsFlip; }
        set { numCardsFlip = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetupGame();
    }

    private void Update()
    {
        // Check if all cards are flipped
        if (allCards.TrueForAll(card => card.isFlipped))
        {
            // All cards are flipped so we win and restart the game
            Debug.Log("You win!");
            RestartGame();

        }
    }

    private void RestartGame()
    {
        if (cardPrefab != null) {
            foreach (Transform child in cardGrid)
            {
                Destroy(child.gameObject);
            }
            allCards.Clear();
            SetupGame();
        }

    }

    private void SetupGame()
    {
        int[] cardIDs = new int[cardFrontSprites.Length * 2];
        numCardsFlip = 0;

        for (int i = 0; i < cardFrontSprites.Length; i++)
        {
            cardIDs[i * 2] = i;
            cardIDs[i * 2 + 1] = i;
        }

        ShuffleArray(cardIDs);



        for (int i = 0; i < cardIDs.Length; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardGrid);
            Card card = cardObj.GetComponent<Card>();
            card.SetupCard(cardIDs[i], cardFrontSprites[cardIDs[i]]);
            allCards.Add(card);
        }
    }

    private void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    public void CardFlipped(Card card)
    {
 
        if (firstFlippedCard == null)
        {
            firstFlippedCard = card;
        }
        else if (secondFlippedCard == null)
        {
            secondFlippedCard = card;
            CheckForMatch();
        }
    }

    private void CheckForMatch()
    {
        if (firstFlippedCard.cardID == secondFlippedCard.cardID)
        {
            // Les cartes correspondent, donc on les laisse affichées
            firstFlippedCard = null;
            secondFlippedCard = null;
            numCardsFlip = 0;
        }
        else
        {
            // Les cartes ne correspondent pas, on les cache après un délai
            StartCoroutine(WaitAndHideCards());
            
        }
    }

    private IEnumerator WaitAndHideCards()
    {
        yield return new WaitForSeconds(1);
        HideCards();
        numCardsFlip = 0;
    }

    private void HideCards()
    {
        firstFlippedCard.HideCard();
        secondFlippedCard.HideCard();
        firstFlippedCard = null;
        secondFlippedCard = null;
    }
}
