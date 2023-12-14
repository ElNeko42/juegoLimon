using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 todos los enum que este relacionados con las cartas ponerlos en este scrip
 */
public enum Tipo
{
    Fe = 1,
    Fuerza = 2,
    Conocimiento = 3
}

public enum CardType { CHAR, EVENT, LUCK, BOSS }

public enum CardLugar { ciudad, bosque, desierto, montaña} //ejemplo de localisaciones se pueden modificar segun el proyecto


public class CardData : MonoBehaviour
{
    [Header("Datos Personaje")]
    public string cardName;
    public string[] cardTexts; //los Textos maximo  60 caracteres incluidos espacios
    public string[] leftOptions;
    public string[] rightOptions; 
    public int[] responsesFeLeft; 
    public int[] responsesFeRight;
    public int[] responsesPuebloLeft;
    public int[] responsesPuebloRight;
    public int[] responsesMilitarLeft;
    public int[] responsesMilitarRight;
    public int[] responsesComidaLeft;
    public int[] responsesComidaRight;
    public int[] DineroLeft;
    public int[] DineroRight;
    [Header("Tipo de Cartas")]
    public CardType cardType;
    public CardType CType { get => cardType; }

    public Tipo statType;
    public Tipo SType { get => statType; }

    public CardLugar lugarType;
    public CardLugar LType { get => lugarType; }

    [Header("Accion dados")]
    public Tipo[] tipoLeft;
    public Tipo[] tipoRight;
    public int[] accionLeft;
    public int[] accionRight;
}
