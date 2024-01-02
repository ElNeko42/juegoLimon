using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public RolPlayer rolPlayer;
    public Player player;
    public TextControl textControl;
    public DiceController diceController;


    void Start()
    {
        rolPlayer = FindObjectOfType<RolPlayer>();
        player = FindObjectOfType<Player>();
        textControl = FindObjectOfType<TextControl>();
        diceController = FindObjectOfType<DiceController>();

        if (textControl.diasTextMesh != null)
        {
            // Obtén el componente TextTranslator del objeto
            TextTranslatorDynamic textTranslatorDia = textControl.diasTextMesh.GetComponent<TextTranslatorDynamic>();

            // Verifica si el componente TextTranslator existe
            if (textTranslatorDia != null)
            {
                textTranslatorDia.SetValue(player.dias);
            }
            else
            {
                Debug.LogError("Componente TextTranslator no encontrado en TextMeshProUGUI");
            }
        }
        else
        {
            Debug.LogError("Objeto TextMeshProUGUI no asignado en GameManager");
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

}
