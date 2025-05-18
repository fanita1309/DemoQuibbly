using System.Collections.Generic;
using UnityEngine;

namespace productions
{
    public enum CardOwner
    {
        Player,
        Enemy
    }

    public class Card : ScriptableObject
    {
        public string cardName;
        public List<ElementType> cardType;
        public Sprite cardSprite;
        public string description;
        public CardOwner owner;

        public enum ElementType
        {
            Fire,
            Earth,
            Water,
            Dark,
            Light,
            Air
        }

        public enum AttackPattern
        {
            Single,
            Multitarget,
            Cross,
            Column,
            Row,
            TwoByTwo,
            FourByFour
        }

        public enum PriorityTarget
        {
            Close,
            Far,
            LeastCurrentHealth,
            MostCurrentHealth,
            MostMaxHealth,
            MostDamage
        }

        public enum SpellType
        {
            Buff,
            Debuff
        }

        public enum AttributeTarget
        {
            health,
            damage,
            range,
            attackPattern,
            damageType,
            cardType,
            priorityTarget
        }
    }
}