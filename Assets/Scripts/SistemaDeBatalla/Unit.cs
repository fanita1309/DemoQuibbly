using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health = 10;
    public int attackPower = 2;
    public CellOwner owner;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} recibió {damage} de daño. Vida restante: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject); // Aquí puedes cambiar por una animación o efecto
    }

    public void Attack(Unit target)
    {
        if ((TurnManager.Instance.IsPlayerTurn() && owner == CellOwner.Player) ||
            (TurnManager.Instance.IsEnemyTurn() && owner == CellOwner.Enemy))
        {
            target.TakeDamage(attackPower);
            TurnManager.Instance.EndTurn(); // Finaliza turno tras atacar
        }
        else
        {
            Debug.Log("⛔ No puedes atacar en este turno.");
        }
    }
}
