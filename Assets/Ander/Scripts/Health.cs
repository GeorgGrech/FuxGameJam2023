using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int MaxHealth = 100;  // Maximum health of the enemy.
    private int CurrentHealth;   // Current health of the enemy.

    [SerializeField] private Image healthBar;
    private void Start()
    {
        // Initialize the current health to the maximum health when the enemy spawns.
        CurrentHealth = MaxHealth;
        
    }


    public void TakeDamage(int damage)
    {
        // Reduce the current health by the amount of damage.
        CurrentHealth -= damage;

        // Ensure that the health doesn't go below zero.
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        // Calculate the new fill amount based on the updated health values.
        float newFillAmount = (float)CurrentHealth / MaxHealth;

        // Update the health bar's fill amount.da
        healthBar.fillAmount = newFillAmount;

        // Check if the enemy's health has dropped to or below zero.
        if (CurrentHealth <= 0)
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

    public void UpdateHealthBar(float enemyMaxHealth, float enemyCurrentHealth)
    {
        healthBar.fillAmount = enemyCurrentHealth / enemyMaxHealth;
    }
}