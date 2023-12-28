using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class LanguageSwitcher : MonoBehaviour
{
    private void Awake()
    {
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        //fill in the value
        dropdown.options = new List<TMP_Dropdown.OptionData>();
        foreach (string language in Data.LANGUAGES)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(language));
        }
        //set the value
        dropdown.onValueChanged.AddListener((int i) => {
            string language = Data.LANGUAGES[i];
            Data.CURRENT_LANGUAGE = language;
            Data.OnLanguageChanged.Invoke();
    });
    }
}
