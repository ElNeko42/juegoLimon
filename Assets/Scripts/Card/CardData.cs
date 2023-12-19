using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 todos los enum que este relacionados con las cartas ponerlos en este scrip
 */
public enum Tipo
{
    Fe,
    Fuerza,
    Sabiduria,
    Suerte
}

public enum CardType { CHAR, EVENT, LUCK, BOSS }

public enum CardLugar { ciudad, bosque, desierto, montaña} //ejemplo de localisaciones se pueden modificar segun el proyecto

[System.Serializable]
public struct CardResponse
{
    public int cambioVidas;
    public int cambioMana;
    public int cambioPueblo;
    public int cambioMilitar;
    public int cambioComida;
    public int cambioDinero;
}

public class CardData : MonoBehaviour
{
    [Header("Datos Personaje")]
    public string cardName;
    public string[] cardTexts; // Los Textos máximo 60 caracteres incluidos espacios
    public string[] leftOptions;
    public string[] rightOptions;
    public CardResponse[] responsesLeftSuccess; 
    public CardResponse[] responsesLeftFail; 
    public CardResponse[] responsesRightSuccess; 
    public CardResponse[] responsesRightFail;

    [Header("Tipo de Cartas")]
    public CardType cardType;
   
    public CardLugar lugarType;

    [Header("Accion dados")]
    //tipo de tirada
    public Tipo[] tipoLeft;
    public Tipo[] tipoRight;
    //resultado que tienes que llegar o superar
    public int[] accionLeft; 
    public int[] accionRight;
}
