using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards; // Array para almacenar los prefabs de las cartas
    private GameObject currentCard; // La carta actual en la escena
    private int currentCardIndex = 0; // Índice de la carta actual
    private Canvas canvas; // Referencia al Canvas

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        LoadRandomCard();
    }

    private void LoadRandomCard()
    {
        if (cards.Length > 0)
        {
            if (currentCard != null)
            {
                Destroy(currentCard); // Destruye la carta actual si existe
            }

            int randomIndex = Random.Range(0, cards.Length);
            currentCard = Instantiate(cards[randomIndex]);
            currentCard.transform.SetParent(canvas.transform, false); // Establece el Canvas como padre
            currentCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Centra la carta
        }
    }


    public void ChangeCard(bool nextCard)
    {
        if (currentCard != null)
        {
            Destroy(currentCard); // Destruye la carta actual
        }

        // Calcula el índice de la nueva carta
        if (nextCard)
        {
            currentCardIndex = (currentCardIndex + 1) % cards.Length;
        }
        else
        {
            currentCardIndex = (currentCardIndex - 1 + cards.Length) % cards.Length;
        }

        // Instancia la nueva carta
        currentCard = Instantiate(cards[currentCardIndex]);
        currentCard.transform.SetParent(canvas.transform, false); // Establece el Canvas como padre
        currentCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Centra la carta en el Canvas
    }


}

