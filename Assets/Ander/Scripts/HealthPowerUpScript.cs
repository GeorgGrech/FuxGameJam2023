using UnityEngine;

public class HealthPowerUpScript : MonoBehaviour
{
    public int healingAmount = 10; // Amount of healing to provide.

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
                // Heal the player.
                playerHealth.Heal(healingAmount);

                // Destroy the health power-up object.
                Destroy(gameObject);
            }
        }
    }
}
