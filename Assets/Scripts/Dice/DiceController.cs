using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    public Sprite[] diceSides;
    private Image diceImage;
    private Animator animator;

    public bool isDiceRolled = false;
    public static bool  cambiosStat = false;
    int diceValue = -1;
    public static int totalValue = 0;

    void Start()
    {
        diceImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void RollDice()
    {
        if (!isDiceRolled)
        {
            animator.enabled = true;
            StartCoroutine(SetDiceResult());
        } 
    }

    IEnumerator SetDiceResult()
    {
        yield return new WaitForSeconds(1); // Espera a que la animacion termine
        diceValue = Random.Range(0, 12);
        diceImage.sprite = diceSides[diceValue-1];
        Debug.Log("dado "+diceValue);
        Debug.Log("dado "+ diceImage.sprite);
        isDiceRolled = true;
        animator.enabled = false;
        Vector3 rotationEuler = transform.rotation.eulerAngles;
        rotationEuler.z = 0;
        transform.rotation = Quaternion.Euler(rotationEuler);
        int valorHabilidad = CardManager.statValue;
        DicePanel dicePanel = this.transform.parent.GetComponent<DicePanel>();
        dicePanel.totalValue.gameObject.SetActive(true);
        totalValue = diceValue + valorHabilidad;
        dicePanel.totalValue.text = totalValue.ToString();
        dicePanel.aceptar.gameObject.SetActive(true);
        cambiosStat = true;
    }
}

