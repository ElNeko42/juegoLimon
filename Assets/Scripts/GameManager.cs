using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int fe = 5;
    public int militar = 5;
    public int pueblo = 5;
    public int comida = 5;
    public int dinero = 1000;
    public int ano = 0;


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
