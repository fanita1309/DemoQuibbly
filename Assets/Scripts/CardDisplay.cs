using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using productions;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages;
    private Color[] typeColors ={
        new Color (0.23f, 0.05f, 0.20f),//weapon
        Color.blue,//defense
        Color.green, //talk
        Color.cyan, //item

    };

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        //update the main card image color based on the first card type
        cardImage.color = typeColors[(int)cardData.cardtype[0]];

        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = $"{cardData.damageMin} - {cardData.damageMax}";
    }

}
