using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    public Sprite[] diceSides;
    private Image diceImage;
    private Animator animator;

    void Start()
    {
        diceImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void RollDice()
    {
        animator.SetTrigger("roll"); // Inicia la animación de lanzamiento
        StartCoroutine(SetDiceResult());
    }

    IEnumerator SetDiceResult()
    {
        yield return new WaitForSeconds(1); // Espera a que la animación termine
        int randomDiceSide = Random.Range(0, 11);
        diceImage.sprite = diceSides[randomDiceSide];
    }
}

