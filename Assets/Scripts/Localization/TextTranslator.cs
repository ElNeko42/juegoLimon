using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextTranslator : MonoBehaviour
{
    TextMeshProUGUI text;
    string key ;
    public static int coins = 0;
    private void Awake()
    {
         text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI component no encontrado en el GameObject.");
            return;
        }

        key = text.text;
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
        _SetText();
        Data.OnLanguageChanged.AddListener(_SetText);

    }
    private void _SetText()
    {
        if (Data.LOCALIZATION[key].ContainsKey(Data.CURRENT_LANGUAGE))
        {
           
           
            string format = Data.LOCALIZATION[key][Data.CURRENT_LANGUAGE];
            text.text = string.Format(format, coins);
        }
        else
        {
            Debug.LogError($"No existe entrada para el idioma actual '{Data.CURRENT_LANGUAGE}' para la clave '{key}'.");
        }
    }

    public void UpdateText()
    {
        string format = Data.LOCALIZATION[key][Data.CURRENT_LANGUAGE];
        text.text = string.Format(format, coins);
    }


}
