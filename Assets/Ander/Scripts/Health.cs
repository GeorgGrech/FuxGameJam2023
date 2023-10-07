using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int MaxHealth;  // Maximum health.
    public int CurrentHealth;   // Current health.
    

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

        // Check if health has dropped to or below zero.
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Dead"); // Add this line for debugging
        Destroy(gameObject);
        
        if (CompareTag("Enemy"))
        {
            GetComponent<EnemyLoot>().DropHealth();

        }
        

    }



    public void UpdateHealthBar(float enemyMaxHealth, float enemyCurrentHealth)
    {
        healthBar.fillAmount = enemyCurrentHealth / enemyMaxHealth;
    }
}