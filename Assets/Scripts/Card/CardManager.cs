using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    [Header("Configuración de Cartas")]
    public GameObject[] cards; 
    private GameObject currentCard; 
    private GameObject LastcurrentCard; 
    private int currentCardIndex = 0;
    private Canvas canvas; 

    [Header("Textos")]
    public TextMeshProUGUI NameTextMesh;
    public TextMeshProUGUI cardTextMesh; 
    public TextMeshProUGUI leftOptionTextMesh; 
    public TextMeshProUGUI rightOptionTextMesh;

    int textIndex = 0;

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
            CardData cardData = currentCard.GetComponent<CardData>();
            Debug.Log("Card Name: " + cardData.cardName);
            Debug.Log("Card Texts: " + cardData.cardTexts[0]);
            NameTextMesh.text = cardData.cardName;
            int randomIndexText = Random.Range(0, cardData.cardTexts.Length);
            cardTextMesh.text = cardData.cardTexts[randomIndexText];
            leftOptionTextMesh.text = cardData.leftOptions[randomIndexText];
            rightOptionTextMesh.text = cardData.rightOptions[randomIndexText];
        }
    }


    public void ChangeCard(bool nextCard)
    {
        if (currentCard != null)
        {
            CardEffect(nextCard);
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
        DisplayRandomCardText();
    }

    public void DisplayRandomCardText()
    {
        CardData cardData = currentCard.GetComponent<CardData>();
        textIndex = Random.Range(0, cardData.cardTexts.Length);
        Debug.Log(textIndex);
        UpdateCardUI(textIndex);
    }

    public void UpdateCardUI(int index)
    {
        CardData cardData = currentCard.GetComponent<CardData>();
        NameTextMesh.text = cardData.cardName;
        cardTextMesh.text = cardData.cardTexts[index];
        leftOptionTextMesh.text = cardData.leftOptions[index];
        rightOptionTextMesh.text = cardData.rightOptions[index];
    }

    void CardEffect(bool rightOptionChosen)
    {
        if (currentCard != null)
        {
            CardData card = currentCard.GetComponent<CardData>();
            int faithCard;
            int knowledgeCard;
            int strengthCard;
            int lemonCard;

            if (rightOptionChosen)
            {
                faithCard = currentCard.GetComponent<CardData>().responsesFeRight[textIndex];
                knowledgeCard = currentCard.GetComponent<CardData>().responsesPuebloRight[textIndex];
                strengthCard = currentCard.GetComponent<CardData>().responsesMilitarRight[textIndex];
                lemonCard = currentCard.GetComponent<CardData>().DineroRight[textIndex];

            } else
            {
                faithCard = currentCard.GetComponent<CardData>().responsesFeLeft[textIndex];
                knowledgeCard = currentCard.GetComponent<CardData>().responsesPuebloLeft[textIndex];
                strengthCard = currentCard.GetComponent<CardData>().responsesMilitarLeft[textIndex];
                lemonCard = currentCard.GetComponent<CardData>().DineroLeft[textIndex];
            }

            GameController.instance.playerFaith += faithCard;
            GameController.instance.playerKnowledge += knowledgeCard;
            GameController.instance.playerStrength += strengthCard;
            GameController.instance.playerLemon += lemonCard;

        }
    }

}

