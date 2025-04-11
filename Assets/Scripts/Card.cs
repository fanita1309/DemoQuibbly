using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace productions
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]

    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardtype;
        public int health;
        public int damageMin;
        public int damageMax;
        public List<DamageType> damageType;

        public enum CardType //tipos de cartas
        {
            Fire,

            Earth,

            Water,

            Air,

        }

        public enum DamageType //tipos de daño
        {
            Fire,

            Earth,

            Water,

            Air,

        }


    }
}
