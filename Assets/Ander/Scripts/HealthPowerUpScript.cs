using UnityEngine;

public class HealthPowerUpScript : MonoBehaviour
{
    public int healingAmount = 10; // Amount of healing to provide.

    public float rotationSpeed = 60f; // Degrees per second

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);

    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the player tag.
        if (other.CompareTag("Player"))
        {
            // Get the Health component of the player (assuming the player has a Health script).
            Health playerHealth = other.GetComponent<Health>();

            // Check if the player has a Health component.
            if (playerHealth != null)
            {
                // Check if the player's current health is less than their maximum health.
                if (playerHealth.CurrentHealth < playerHealth.MaxHealth)
                {
                    // Heal the player.
                    playerHealth.Heal(healingAmount);

                    // Destroy the health power-up object.
                    Destroy(gameObject);
                }
                // You can add an optional else block to provide feedback to the player if they have full health.
            }
        }
    }
}
