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
    int diceValue = -1;

    void Start()
    {
        diceImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void RollDice()
    {
        if (!isDiceRolled)
        {
            animator.SetTrigger("roll"); // Inicia la animación de lanzamiento
            StartCoroutine(SetDiceResult());
        } 
    }

    IEnumerator SetDiceResult()
    {
        yield return new WaitForSeconds(1); // Espera a que la animación termine
        diceValue = Random.Range(0, 12);
        diceImage.sprite = diceSides[diceValue];
        isDiceRolled = true;
        DicePanel dicePanel = this.transform.parent.GetComponent<DicePanel>();
        dicePanel.totalValue.gameObject.SetActive(true);
        dicePanel.totalValue.text = (diceValue + int.Parse(dicePanel.statValue.text)).ToString();
        dicePanel.aceptar.gameObject.SetActive(true);
    }
}

