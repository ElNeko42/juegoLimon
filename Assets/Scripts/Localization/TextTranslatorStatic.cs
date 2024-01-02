using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextTranslatorStatic : MonoBehaviour
{
    public string key;
    private void Awake()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI component no encontrado en el GameObject.");
            return;
        }
        if (!Data.LOCALIZATION.ContainsKey(key))
        {
            Debug.LogError($"La clave '{key}' no existe en el diccionario LOCALIZATION.");
            return;
        }

        if (!Data.LOCALIZATION[key].ContainsKey(Data.CURRENT_LANGUAGE))
        {
            Debug.LogError($"No existe entrada para el idioma actual '{Data.CURRENT_LANGUAGE}' para la clave '{key}'.");
            return;
        }
        text.text = Data.LOCALIZATION[key][Data.CURRENT_LANGUAGE];
    }
}
