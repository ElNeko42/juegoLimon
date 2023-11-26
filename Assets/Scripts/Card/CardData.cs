using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
