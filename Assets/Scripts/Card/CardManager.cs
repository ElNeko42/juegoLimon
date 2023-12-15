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
    public GameObject dicePanel;

    [Header("Textos")]
    public TextMeshProUGUI NameTextMesh;
    public TextMeshProUGUI cardTextMesh; 
    public TextMeshProUGUI leftOptionTextMesh; 
    public TextMeshProUGUI rightOptionTextMesh;
    public TextMeshProUGUI vidaTextMesh;
    public TextMeshProUGUI manaTextMesh;
    public TextMeshProUGUI comidaTextMesh;
    public TextMeshProUGUI dineroTextMesh;
    public TextMeshProUGUI diasTextMesh;

    int textIndex = 0;
    bool diceUsed = false;

    private void Awake()
    {
        canvas = FindObjectsOfType<Canvas>()[0];
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
            //CardEffect(nextCard);
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
        MakeOptionsInvisible();
        GameManager.instance.player.dias++;
        diasTextMesh.text = "dias: "+ GameManager.instance.player.dias.ToString();

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

    // TODO: hay que cambiar esta funciones con el nuevo planteamiento del juego 

    //public void CardEffect(bool optionChosen)
    //{
    //    if (currentCard != null)
    //    {
    //        CardData cardData = currentCard.GetComponent<CardData>();

    //        switch (cardData.CType)
    //        {
    //            case CardType.CHAR:
    //                CardEffectChar(optionChosen, cardData);
    //                break;
    //            case CardType.EVENT:
    //                CardEffectEvent(optionChosen, cardData);
    //                break;
    //            case CardType.LUCK:
    //                CardEffectLuck(optionChosen, cardData);
    //                break;
    //            case CardType.BOSS:
    //                CardEffectBoss(optionChosen, cardData);
    //                break;

    //        }     
    //    }
    //}

    //void CardEffectChar(bool optionChosen, CardData cardData)
    //{
    //    // Calcula los cambios en función de la elección
    //    int changeFe = optionChosen ? cardData.responsesVidasRight[textIndex] : cardData.responsesVidasLeft[textIndex];
    //    int changeMana = optionChosen ? cardData.responsesMilitarRight[textIndex] : cardData.responsesMilitarLeft[textIndex];
    //    int changeComida = optionChosen ? cardData.responsesComidaRight[textIndex] : cardData.responsesComidaLeft[textIndex];
    //    int changeDinero = optionChosen ? cardData.DineroRight[textIndex] : cardData.DineroLeft[textIndex];

    //    // Actualiza las variables en GameManager
    //    GameManager.instance.vida += changeFe;
    //    GameManager.instance.mana += changeMana;
    //    GameManager.instance.comida += changeComida;

    //    // Actualiza dinero asegurándose de que no exceda los límites establecidos
    //    int newDineroValue = GameManager.instance.dinero + changeDinero;
    //    GameManager.instance.dinero = Mathf.Clamp(newDineroValue, -99999, 99999);

    //    // Actualiza los textos
    //    vidaTextMesh.text = GameManager.instance.vida.ToString();
    //    manaTextMesh.text = GameManager.instance.mana.ToString();
    //    comidaTextMesh.text = GameManager.instance.comida.ToString();
    //    dineroTextMesh.text = GameManager.instance.dinero.ToString();
    //}

    //void CardEffectEvent(bool optionChosen, CardData cardData)
    //{
    //    if (!optionChosen)
    //    {
    //        string statText = "";
    //        string statValue = "";
    //        Debug.Log("Carta es nula: " + cardData==null);
    //        switch (cardData.SType)
    //        {
    //            case (Tipo.Fe):
    //                statText = Tipo.Fe.ToString();
    //                statValue = GameManager.instance.vida.ToString(); 
    //                break;
    //            case (Tipo.Conocimiento):
    //                statText = Tipo.Conocimiento.ToString();
    //                statValue = GameManager.instance.mana.ToString();
    //                break;
    //            case (Tipo.Fuerza):
    //                statText = Tipo.Fuerza.ToString();
    //                statValue = GameManager.instance.comida.ToString();
    //                break;
    //        }
    //        DicePanel panel = dicePanel.GetComponent<DicePanel>();
    //        panel.ShowPannel();
    //        panel.statText.text = statText;
    //        panel.statValue.text = statValue;
    //        StartCoroutine(DiceActionCoroutine());

    //    } else
    //    {
    //        //GameOver
    //    }
    //}

    //IEnumerator DiceActionCoroutine()
    //{

    //    while (dicePanel.gameObject.activeSelf)
    //    {
    //        yield return null;

    //    }
    //    if (!dicePanel.gameObject.activeSelf)
    //    {
    //        cardTextMesh.gameObject.SetActive(true);
    //        leftOptionTextMesh.gameObject.SetActive(true);
    //        rightOptionTextMesh.gameObject.SetActive(true);
    //    } else
    //    {
    //        cardTextMesh.gameObject.SetActive(false);
    //        leftOptionTextMesh.gameObject.SetActive(false);
    //        rightOptionTextMesh.gameObject.SetActive(false);
    //    }

    //    //if (!diceUsed)
    //    //{
    //    //    diceUsed = true;
    //    //    ShowDiceResult();
    //    //} else
    //    //{

    //    //}
    //}


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

    void OnEnable()
    {
        DragDropScript.OnCardDrag += UpdateOptionVisibility;
    }
}

