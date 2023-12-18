using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    private Class[] allClasses; // Array de todas las clases
    private int currentIndex = 0; // �ndice actual

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
        if(claseTextMesh != null)
        {
        DisplayClassInfo();
        Debug.Log(allClasses.Length);
        }
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

    // Mostrar informaci�n de la clase actual
    private void DisplayClassInfo()
    {
        
        Class currentClass = allClasses[currentIndex];
        claseTextMesh.text = currentClass.ToString();
       
    }
}
