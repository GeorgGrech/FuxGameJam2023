using UnityEngine;

public class Damage : MonoBehaviour
{
    public string playerTag = "Player"; // The tag of the player GameObject.
    public int damageAmount = 10; // Amount of damage to inflict.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the player tag.
        if (other.CompareTag(playerTag))
        {
            // Get the Health component of the player (assuming the player has a Health script).
            Health playerHealth = other.GetComponent<Health>();

            // Check if the player has a Health component.
            if (playerHealth != null)
            {
                // Inflict damage on the player.
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
