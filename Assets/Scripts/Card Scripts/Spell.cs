using System.Collections.Generic;
using productions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Card/Spell")]
public class Spell : Card
{
    public SpellType spellType;
    public List<AttributeTarget> attributeTarget;
    public List<int> attributeChangeAmount;
    public AttackPattern attackPatternToChangeTo;
    public ElementType damageTypeToChangeTo;
    public ElementType cardElementTypeToChangeTo;
    public PriorityTarget priorityTargetToChangeTo;
}
