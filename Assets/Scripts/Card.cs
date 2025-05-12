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
        public Sprite cardSprite;
        public List<DamageType> damageType;
        public GameObject prefab;
        public int range;
        public AttackPattern attackPattern;
        public PriorityTarget priorityTarget;

        public enum CardType //tipos de cartas
        {
            Weapon,

            Defense,

            Talk,

            Item,

        }

        public enum DamageType //tipos de daño
        {
            Weapon,

            Defense,

            Talk,

            Item,

        }

        public enum AttackPattern //tipo de ataque
        {
            Single,

            Multitarget,

            Cross,

            Column,

            Row,

            TwoByTwo,

            FourByFour,

        }

        public enum PriorityTarget 
        {
            Close,

            Far,

            LeastCurrentHealth,

            MostCurrentHealth,

            MostMaxHealth,

            MostDamage,

        }


    }
}
