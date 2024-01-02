using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public static class Data
{
    public static string CURRENT_LANGUAGE = "es";
    public static Dictionary<string, Dictionary<string, string>> LOCALIZATION = new Dictionary<string, Dictionary<string, string>>()
    {
          {"aceptar_key", new Dictionary<string, string>()
        {
                {"en", "accept"},
                { "es","Aceptar"},
            }
        },
        {"play_key", new Dictionary<string, string>()
        {
                {"en", "Play"},
                { "es","Jugar"},
            }
        },
        {"option_key", new Dictionary<string, string>()
        {
                {"en", "Option"},
                { "es","Opciones"},
            }
        },
        {
        "monedas_key", new Dictionary<string, string>()
            {
                {"en", "You have {0} coins"},
                { "es", "Tienes {0} monedas"},
            }
        },   
        {
        "dias_key", new Dictionary<string, string>()
            {
                {"en", "Days: {0}"},
                { "es", "Dias: {0}"},
            }
        },

    };
    public static string[] LANGUAGES = new string[] { "es", "en" };

    //use singleton pattern
    private static UnityEvent _onLanguageChanged;
    public static UnityEvent OnLanguageChanged
    {
        get
        {
            if (_onLanguageChanged == null)
            {
                _onLanguageChanged = new UnityEvent();
            }
            return _onLanguageChanged;
        }
    }
}
