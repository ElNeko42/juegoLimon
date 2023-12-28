using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botones : MonoBehaviour
{
    public TextTranslator textTranslator; // Asigna esto a través del Inspector de Unity

    public void mas()
    {
        TextTranslator.coins++;
        textTranslator.UpdateText(); // Actualiza el texto en la pantalla
        Debug.Log("Monedas: " + TextTranslator.coins); // Esto es solo para verificación en consola
    }

    public void menos()
    {
        TextTranslator.coins--;
        textTranslator.UpdateText(); // Actualiza el texto en la pantalla
        Debug.Log("Monedas: " + TextTranslator.coins); // Esto es solo para verificación en consola
    }
}

