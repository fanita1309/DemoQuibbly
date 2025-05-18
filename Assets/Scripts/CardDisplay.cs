using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using productions;
using System;

public class CardDisplay : MonoBehaviour
{
    //card elements
    public Card cardData;
    public Image cardImage;
    public TMP_Text nameText;
    public Image[] typeImages;
    public Image displayImage;
    public GameObject characterElements;
    public GameObject spellElements;
    public GameObject characterCardLabel;
    public GameObject spellCardLabel;
    public TMP_Text descriptionText;

    //character elements

    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image damageImage;

    //spell elements
    public GameObject[] spellTypeLabels;
    public GameObject[] attributeTargetSymbols;
    public float attributeSymbolSpacing = 10f;
    public TMP_Text attributeChangeAmountText;


    private Color[] cardColors ={
        new Color (0.23f, 0.05f, 0.20f),//weapon
        Color.blue,//defense
        Color.green, //talk
        Color.cyan, //item
        Color.magenta,
        Color.red,

    };
    private Color[] typeColors ={
        new Color (0.50f, 0.05f, 0.20f),//weapon
        Color.red,//defense
        Color.blue, //talk
        Color.green, //item
        Color.black,
        Color.white,
    };

    void Update()
    {
        UpdateCardDisplay();    
    }

    public void UpdateCardDisplay()
    {
        //all card changes
        cardImage.color = cardColors[(int)cardData.cardType[0]];
        nameText.text = cardData.cardName;
        displayImage.sprite = cardData.cardSprite;
        descriptionText.text = cardData.description;

        //update type cards
        for (int i = 0; i < typeImages.Length; i++)
        {
            if (i < cardData.cardType.Count) {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }

        }

        //specific card changes
        if (cardData is Character characterCard)
        {
            UpdateDisplayCharacterCard(characterCard);
        }
        else  if (cardData is Spell spellCard)
        {
            UpdateDisplaySpellCard(spellCard);
        }

    }

    private void UpdateDisplayCharacterCard(Character characterCard)
    {
        spellElements.SetActive(false);
        characterElements.SetActive(true);
        characterCardLabel.SetActive(true);

        healthText.text = characterCard.health.ToString();
        damageText.text = $"{characterCard.damageMin} - {characterCard.damageMax}";
        damageImage.color = typeColors[(int)characterCard.damageType[0]];
    }

    private void UpdateDisplaySpellCard(Spell spellCard)
    {
        characterElements.SetActive(false);
        spellElements.SetActive(true);
        spellCardLabel.SetActive(true);
        
        //set correct spell type label
        foreach (GameObject label in spellTypeLabels)
        {
            label.SetActive(false);
        }
        spellTypeLabels[(int)spellCard.spellType].SetActive(true);

        //reset and update attribute target symbols
        foreach (GameObject symbol in attributeTargetSymbols)
        {
            symbol.SetActive(false);
        }

        for (int i = 0; i < spellCard.attributeTarget.Count; i++)
        {
            GameObject currentSymbol = attributeTargetSymbols[(int)spellCard.attributeTarget[i]];
            currentSymbol.SetActive(true);
            float newYPosition = i * attributeSymbolSpacing;
            currentSymbol.transform.localPosition = new Vector3(0, newYPosition, 0);
        }

        //display attribute change amounts
        attributeChangeAmountText.text = string.Join(",", spellCard.attributeChangeAmount);
    }
}
