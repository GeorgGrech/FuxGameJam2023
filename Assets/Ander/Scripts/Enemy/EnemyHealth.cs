using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth = 100;  // Maximum health of the enemy.
    private int enemyCurrentHealth;   // Current health of the enemy.

    private void Start()
    {
        // Initialize the current health to the maximum health when the enemy spawns.
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Method to handle taking damage.
    public void TakeDamage(int damage)
    {
        // Reduce the current health by the amount of damage.
        enemyCurrentHealth -= damage;

        // Check if the enemy's health has dropped to or below zero.
        if (enemyCurrentHealth <= 0)
        {
            // Call a method to handle enemy death (e.g., play death animation, remove the enemy, or trigger some event).
            Die();
        }
    }

    // Method to handle enemy death.
    private void Die()
    {
        Destroy(gameObject);
    }
}