using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace productions
{
    public class SpellEffectApplier
    {
        
        public static void ApplySpell(Spell spell, CharacterStats targetStats)
        {
             for (int i = 0; i < spell.attributeTarget.Count; i++)
            {
                int changeAmount = spell.attributeChangeAmount.Count > i ? spell.attributeChangeAmount[i] : 0;
                ApplyEffectToAttribute(spell, spell.spellType, spell.attributeTarget[i], changeAmount, targetStats);
            }
        }

        public static void ApplyEffectToAttribute (Spell spell, Spell.SpellType spellType, Card.AttributeTarget attributeTarget, int changeAmount, CharacterStats targetStats)
        {
            int finalChangeAmount = spellType == Spell.SpellType.Buff ? changeAmount : -changeAmount;   
            switch (attributeTarget)
            {
                case Card.AttributeTarget.health:
                    targetStats.health += finalChangeAmount; 
                    break;
                case Card.AttributeTarget.damage:
                    targetStats.damageMax += finalChangeAmount;
                    targetStats.damageMin += finalChangeAmount;
                    break;
                case Card.AttributeTarget.range:
                    targetStats.range += finalChangeAmount;
                    break;
                case Card.AttributeTarget.attackPattern:
                    targetStats.attackPattern = spell.attackPatternToChangeTo;
                    break;
                case Card.AttributeTarget.damageType:
                    targetStats.damageType[0] = spell.damageTypeToChangeTo;
                    break;
                case Card.AttributeTarget.cardType:
                    targetStats.cardType.Add(spell.cardElementTypeToChangeTo);
                    break;
                case Card.AttributeTarget.priorityTarget:
                    targetStats.priorityTarget = spell.priorityTargetToChangeTo;
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("Attribute Target Not Implemented");
                    break;
            }
            ClampCharacterStats(targetStats);
        }

        private static void ClampCharacterStats(CharacterStats stats)
        {
            stats.health = Mathf.Max(stats.health, 0);
            stats.damageMin = Mathf.Max(stats.damageMin, 0);
            stats.damageMax = Mathf.Max(stats.damageMax, stats.damageMin);
            stats.range = Mathf.Max(stats.range, 1);
        }
    }
}
