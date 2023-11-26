using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards; // Array para almacenar los prefabs de las cartas
    private GameObject currentCard; // La carta actual en la escena
    private GameObject LastcurrentCard; // la carta anterior
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
            LastcurrentCard = currentCard; // Guarda la última carta antes de destruirla
        }
    }


    public void ChangeCard(bool nextCard)
    {
        if (currentCard != null)
        {
            LastcurrentCard = currentCard; // Guarda la última carta antes de destruirla
            Destroy(currentCard); // Destruye la carta actual
        }

        int newIndex;
        do
        {
            // Calcula el índice de la nueva carta
            if (nextCard)
            {
                newIndex = (currentCardIndex + 1) % cards.Length;
            }
            else
            {
                newIndex = (currentCardIndex - 1 + cards.Length) % cards.Length;
            }
        } while (cards[newIndex] == LastcurrentCard); // Verifica que la nueva carta no sea la misma que la última

        // Actualiza el índice de la carta actual
        currentCardIndex = newIndex;

        // Instancia la nueva carta
        currentCard = Instantiate(cards[currentCardIndex]);
        currentCard.transform.SetParent(canvas.transform, false); // Establece el Canvas como padre
        currentCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Centra la carta en el Canvas
    }



}

