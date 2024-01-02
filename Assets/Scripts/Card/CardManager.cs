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


    int textIndex = 0;
    bool diceUsed = false;
    public static int statValue = 0;
    public static int totalvalue = 0;
    int valorObjetivo = 0;

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
        MakeOptionsInvisible();
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null");
        }
        else if (GameManager.instance.player == null)
        {
            Debug.LogError("GameManager player is null");
        }
        Debug.Log("dias: " + GameManager.instance.player.dias);
        GameManager.instance.player.dias++;
        Debug.Log("dias: " + GameManager.instance.player.dias);
        TextTranslatorDynamic textTranslatorDia = GameManager.instance.textControl.diasTextMesh.GetComponent<TextTranslatorDynamic>();
        textTranslatorDia.SetValue(GameManager.instance.player.dias);
        textTranslatorDia.UpdateText();
        // GameManager.instance.textControl.diasTextMesh.text = "dias: " + GameManager.instance.player.dias.ToString();

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

    public void CardEffect(bool optionChosen)
    {
        if (currentCard != null)
        {
            CardData cardData = currentCard.GetComponent<CardData>();

            switch (cardData.cardType)
            {
                case CardType.CHAR:
                    CardEffectChar(optionChosen, cardData);
                    break;
                    //case CardType.EVENT:
                    //    CardEffectEvent(optionChosen, cardData);
                    //    break;
                    //case CardType.LUCK:
                    //    CardEffectLuck(optionChosen, cardData);
                    //    break;
                    //case CardType.BOSS:
                    //    CardEffectBoss(optionChosen, cardData);
                    //    break;

            }
        }
    }

    void CardEffectChar(bool optionChosen, CardData cardData)
    {
        // Obtiene la estructura de respuesta correcta en función de la elección
        GameManager gameManager = GameManager.instance;
        int valorObjetivo = optionChosen ? cardData.accionRight[textIndex] : cardData.accionLeft[textIndex];

        // Obtiene el valor de la estadística correspondiente
        string statPoints = optionChosen ? gameManager.rolPlayer.GetPuntosHabilidad(cardData.tipoRight[textIndex]).ToString() : gameManager.rolPlayer.GetPuntosHabilidad(cardData.tipoLeft[textIndex]).ToString();
        statValue = int.Parse(statPoints);
        // Obtiene el texto de la estadística correspondiente
        string statText = optionChosen ? cardData.tipoRight[textIndex].ToString() + " " + statPoints : cardData.tipoLeft[textIndex].ToString() + " " + statPoints;
        DicePanel panel = dicePanel.GetComponent<DicePanel>();
        panel.ShowPannel();
        panel.statText.text = statText;
        panel.condicion.text = "Para ganar, el total debe ser mayor a " + valorObjetivo.ToString();
       StartCoroutine(DiceActionCoroutine());
        totalvalue = DiceController.totalValue;
        bool esExito = totalvalue >= valorObjetivo;
        // Obtiene la estructura de respuesta correcta en función de la elección
        CardResponse response = esExito ?
        (optionChosen ? cardData.responsesRightSuccess[textIndex] : cardData.responsesLeftSuccess[textIndex]) :
        (optionChosen ? cardData.responsesRightFail[textIndex] : cardData.responsesLeftFail[textIndex]);
        //debug de los responses
        //Debug.Log("cambioVidas: " + response.cambioVidas);
        //Debug.Log("cambioMana: " + response.cambioMana);
        //Debug.Log("cambioComida: " + response.cambioComida);
        //Debug.Log("cambioDinero: " + response.cambioDinero);
        StartCoroutine(AplicarRespuestaDespuesDeDados(optionChosen, cardData));


    }

    void CardEffectEvent(bool optionChosen, CardData cardData)
    {
        GameManager gameManager = GameManager.instance;

        if (!optionChosen)
        {
            string statText = "";
            Debug.Log("Carta es nula: " + cardData == null);
            //switch (cardData.statType)
            //{
            //    case (Tipo.Fe):
            //        statText = Tipo.Fe.ToString();
            //        statValue = gameManager.player.playerVida.ToString();
            //        break;
            //    case (Tipo.Conocimiento):
            //        statText = Tipo.Conocimiento.ToString();
            //        statValue = gameManager.player.playerMana.ToString();
            //        break;
            //    case (Tipo.Fuerza):
            //        statText = Tipo.Fuerza.ToString();
            //        statValue = gameManager.player.playerComida.ToString();
            //        break;
            //}
            DicePanel panel = dicePanel.GetComponent<DicePanel>();
            panel.ShowPannel();
            panel.statText.text = statText;
            StartCoroutine(DiceActionCoroutine());

        }
        else
        {
            //GameOver
        }
    }

    IEnumerator DiceActionCoroutine()
    {

        while (dicePanel.gameObject.activeSelf)
        {
            yield return null;

        }
        if (!dicePanel.gameObject.activeSelf)
        {
            cardTextMesh.gameObject.SetActive(true);
            leftOptionTextMesh.gameObject.SetActive(true);
            rightOptionTextMesh.gameObject.SetActive(true);
        }
        else
        {
            cardTextMesh.gameObject.SetActive(false);
            leftOptionTextMesh.gameObject.SetActive(false);
            rightOptionTextMesh.gameObject.SetActive(false);
        }

        //if (!diceUsed)
        //{
        //    diceUsed = true;
        //    ShowDiceResult();
        //} else
        //{

        //}
    }

    IEnumerator AplicarRespuestaDespuesDeDados(bool optionChosen, CardData cardData)
    {
        GameManager gameManager = GameManager.instance;
        // Espera a que se complete la tirada de dados
        yield return new WaitUntil(() => DiceController.cambiosStat);

        // Lógica para determinar si la tirada es un éxito y aplicar la respuesta
        int valorObjetivo = optionChosen ? cardData.accionRight[textIndex] : cardData.accionLeft[textIndex];
        bool esExito = DiceController.totalValue >= valorObjetivo;

        CardResponse response = esExito ?
            (optionChosen ? cardData.responsesRightSuccess[textIndex] : cardData.responsesLeftSuccess[textIndex]) :
            (optionChosen ? cardData.responsesRightFail[textIndex] : cardData.responsesLeftFail[textIndex]);

        // Aplicar la respuesta
        gameManager.player.playerVida += response.cambioVidas;
        Debug.Log("cambioVidas: " + gameManager.player.playerVida);
        gameManager.player.playerMana += response.cambioMana;
        Debug.Log("cambioMana: " + gameManager.player.playerMana);
        gameManager.player.playerComida += response.cambioComida;
        Debug.Log("cambioComida: " + gameManager.player.playerComida);
        gameManager.player.playerDinero = Mathf.Clamp(gameManager.player.playerDinero + response.cambioDinero, -99999, 99999);
        
        // Actualizar los textos
        gameManager.textControl.vidaTextMesh.text = gameManager.player.playerVida.ToString();
        gameManager.textControl.manaTextMesh.text = gameManager.player.playerMana.ToString();
        gameManager.textControl.comidaTextMesh.text = gameManager.player.playerComida.ToString();
        gameManager.textControl.dineroTextMesh.text = gameManager.player.playerDinero.ToString();

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

    void OnEnable()
    {
        DragDropScript.OnCardDrag += UpdateOptionVisibility;
    }
}

