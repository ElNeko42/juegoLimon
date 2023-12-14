using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    //variables en desuso en esta clase se mueve a al script de Player antes de eliminar hay que ver donde se estaban usando
    //en este scrip vamos a hacer el control de del juego Interfaz de Usuario y Menús,
    //Control del Estado del Juego,Gestión de Puntajes y
    //NivelesGestión de Eventos del Juego,Singleton Pattern,Control de Audio y Música
    public string playerName = string.Empty;
    public int playerStrength = 0;
    public int playerKnowledge = 0;
    public int playerFaith = 0;
    public int playerLuck = 0;

    private Class[] allClasses; // Array de todas las clases
    private int currentIndex = 0; // Índice actual

    public TextMeshProUGUI claseTextMesh;


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
        allClasses = (Class[])System.Enum.GetValues(typeof(Class));
        DisplayClassInfo();
        Debug.Log(allClasses.Length);
    }


    // Llamado para ir al siguiente jugador/clase
    public void NextClass()
    {
        currentIndex = (currentIndex + 1) % allClasses.Length;
        DisplayClassInfo();
    }

    // Llamado para ir a la clase anterior
    public void PreviousClass()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = allClasses.Length - 1;
        DisplayClassInfo();
    }

    // Mostrar información de la clase actual
    private void DisplayClassInfo()
    {
        
        Class currentClass = allClasses[currentIndex];
        Debug.Log("Clase Actual: " + currentClass.ToString());
        claseTextMesh.text = currentClass.ToString();
        // Aquí puedes actualizar la UI con la información de currentClass
    }
}
