using System.Collections.Generic;
using productions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Card", menuName = "Card/Character")]
public class Character : Card
{
    public int health;
    public int damageMin;
    public int damageMax;
    public List<Card.ElementType> damageType;
    public GameObject prefab;
    public int range;
    public Card.AttackPattern attackPattern;
    public Card.PriorityTarget priorityTarget;
}
