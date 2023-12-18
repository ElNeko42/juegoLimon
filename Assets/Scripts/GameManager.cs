using System.Collections;
using System.Collections.Generic;
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
