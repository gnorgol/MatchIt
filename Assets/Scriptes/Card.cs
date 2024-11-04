using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public int cardID;
    public Image cardImage;
    public Sprite frontSprite;
    public Sprite backSprite;


    public bool isFlipped = false;
    private bool isAnimating = false;

    public void SetupCard(int id, Sprite front)
    {
        cardID = id;
        frontSprite = front;
        cardImage.sprite = backSprite;
        isFlipped = false;
    }

    public void FlipCard()
    {
        if (isFlipped || isAnimating || GameManager.Instance.NumCardsFlip >= 2)
        return;
        GameManager.Instance.IsAnimating = true;

        StartCoroutine(FlipAnimation());



    }

    private IEnumerator FlipAnimation()
    {
        isAnimating = true;
        float duration = 0.5f; // Durée de l'animation
        float halfDuration = duration / 2;
        float time = 0;


        GameManager.Instance.NumCardsFlip++;

        // Premier demi-tour (fermeture de la carte)
        while (time < halfDuration)
        {
            float angle = Mathf.Lerp(0, 90, time / halfDuration);
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time += Time.deltaTime;
            yield return null;
        }

        // À mi-chemin, changer l'image de la carte
        cardImage.sprite = frontSprite;
        isFlipped = true;

        // Deuxième demi-tour (ouverture de la carte)
        time = 0;
        while (time < halfDuration)
        {
            float angle = Mathf.Lerp(90, 0, time / halfDuration);
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0, 0, 0); // Assurer que la carte est bien alignée
        isAnimating = false;

        GameManager.Instance.CardFlipped(this); // Notifier le gestionnaire de l'événement de retournement

    }

    public void HideCard()
    {
        if (isAnimating) return;
        StartCoroutine(HideAnimation());
    }
    private IEnumerator HideAnimation()
    {
        isAnimating = true;
        float duration = 0.5f; // Durée de l'animation
        float halfDuration = duration / 2;
        float time = 0;

        // Premier demi-tour (fermeture de la carte)
        while (time < halfDuration)
        {
            float angle = Mathf.Lerp(0, 90, time / halfDuration);
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time += Time.deltaTime;
            yield return null;
        }

        // À mi-chemin, changer l'image de la carte
        cardImage.sprite = backSprite;
        isFlipped = false;

        // Deuxième demi-tour (ouverture de la carte)
        time = 0;
        while (time < halfDuration)
        {
            float angle = Mathf.Lerp(90, 0, time / halfDuration);
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0, 0, 0); // Assurer que la carte est bien alignée
        isAnimating = false;
    }
}
