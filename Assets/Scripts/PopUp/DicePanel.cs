using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DicePanel : MonoBehaviour
{

    public TMP_Text statText;
    public TMP_Text statValue;
    public TMP_Text totalValue;
    public Button aceptar;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
