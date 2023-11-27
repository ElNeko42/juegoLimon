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
    public TextMeshProUGUI feTextMesh;
    public TextMeshProUGUI militarTextMesh;
    public TextMeshProUGUI puebloTextMesh;
    public TextMeshProUGUI comidaTextMesh;
    public TextMeshProUGUI dineroTextMesh;

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
            NameTextMesh.text = cardData.cardName;
            int randomIndexText = Random.Range(0, cardData.cardTexts.Length);
            cardTextMesh.text = cardData.cardTexts[randomIndexText];
            leftOptionTextMesh.text = cardData.leftOptions[randomIndexText];
            rightOptionTextMesh.text = cardData.rightOptions[randomIndexText];
            MakeOptionsInvisible();

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
        DisplayRandomCardText();
        CardEffect(nextCard);
        MakeOptionsInvisible();

    }

    public void DisplayRandomCardText()
    {
        CardData cardData = currentCard.GetComponent<CardData>();
        textIndex = Random.Range(0, cardData.cardTexts.Length);
        
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

    public void CardEffect(bool optionChosen)
    {
        if (currentCard != null)
        {
            CardData cardData = currentCard.GetComponent<CardData>();

            // Calcula los cambios en función de la elección
            int changeFe = optionChosen ? cardData.responsesFeRight[textIndex] : cardData.responsesFeLeft[textIndex];
            int changeMilitar = optionChosen ? cardData.responsesPuebloRight[textIndex] : cardData.responsesPuebloLeft[textIndex];
            int changePueblo = optionChosen ? cardData.responsesMilitarRight[textIndex] : cardData.responsesMilitarLeft[textIndex];
            int changeComida = optionChosen ? cardData.responsesComidaRight[textIndex] : cardData.responsesComidaLeft[textIndex];
            int changeDinero = optionChosen ? cardData.DineroRight[textIndex] : cardData.DineroLeft[textIndex];

            // Actualiza las variables en GameManager
            GameManager.instance.fe += changeFe;
            GameManager.instance.militar += changeMilitar;
            GameManager.instance.pueblo += changePueblo;
            GameManager.instance.comida += changeComida;

            // Actualiza dinero asegurándose de que no exceda los límites establecidos
            int newDineroValue = GameManager.instance.dinero + changeDinero;
            GameManager.instance.dinero = Mathf.Clamp(newDineroValue, -99999, 99999);

            // Actualiza los textos
            feTextMesh.text = GameManager.instance.fe.ToString();
            militarTextMesh.text = GameManager.instance.militar.ToString();
            puebloTextMesh.text = GameManager.instance.pueblo.ToString();
            comidaTextMesh.text = GameManager.instance.comida.ToString();
            dineroTextMesh.text = GameManager.instance.dinero.ToString();
        }
    }

    void OnEnable()
    {
        DragDropScript.OnCardDrag += UpdateOptionVisibility;
    }

    void OnDisable()
    {
        DragDropScript.OnCardDrag -= UpdateOptionVisibility;
    }
    public void UpdateOptionVisibility(float positionX)
    {
        float threshold = 180.0f; 
        leftOptionTextMesh.alpha = positionX < -threshold ? 1.0f : 0.0f;
        rightOptionTextMesh.alpha = positionX > threshold ? 1.0f : 0.0f;
    }

    public void MakeOptionsInvisible()
    {
        leftOptionTextMesh.alpha = 0.0f;
        rightOptionTextMesh.alpha = 0.0f;
    }

}

