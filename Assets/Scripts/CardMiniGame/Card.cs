using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardData cardData;
    public TextMeshProUGUI name;
    public TextMeshProUGUI descripton;

    public CardData CardData
    {
        get { if (cardData != null)
            {
                return cardData;
            }
            else
            {
                return cardData = new CardData();
            }
        }
        
        set
        {
            cardData = value;
        }

    }
}
