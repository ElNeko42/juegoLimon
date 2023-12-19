using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DicePanel : MonoBehaviour
{

    public TMP_Text statText; 
    public TMP_Text totalValue;
    public TMP_Text condicion;
    public Button aceptar;
  

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowPannel()
    {
        this.gameObject.SetActive(true);
        totalValue.gameObject.SetActive(false);
        aceptar.gameObject.SetActive(false);
       
    }

    public void HidePannel()
    {
        this.gameObject.SetActive(false);
        GetComponentInChildren<DiceController>().isDiceRolled = false;
    }
}
