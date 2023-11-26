using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public string playerName = string.Empty;
    public int playerStrength = 0;
    public int playerKnowledge = 0;
    public int playerFaith = 0;
    public int playerLuck = 0;
    public int playerLemon = 10000;

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

    private void Update()
    {

    }
}
