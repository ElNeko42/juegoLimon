using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public Canvas canvasGame;
    public Canvas canvasPlayerStatistics;

    public void OnButtonPressed()
    {
        Debug.Log("estoy en el boton");
        
            canvasGame.gameObject.SetActive(false);
      
            canvasPlayerStatistics.gameObject.SetActive(true);
       
    }
}

