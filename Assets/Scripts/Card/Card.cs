using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardData cardData;

    public void SetCardData(CardData newCardData)
    {
        cardData = newCardData;
    }
}

