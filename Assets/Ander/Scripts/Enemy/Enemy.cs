using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage the enemy deals to the player.
    public GameObject triggerObject; // Reference to the trigger object.
    private Fighter playerFighter;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the triggerObject.
        if (other.gameObject == triggerObject)
        {
            // Get the Health component of the player.
            Health playerHealth = other.GetComponent<Health>();

            // Get the Fighter component of the player.
            if (playerHealth != null)
            {
                // Check if the player has a Fighter component and is defending.
                playerFighter = other.GetComponent<Fighter>();
                if (playerFighter == null || !playerFighter.isDefending)
                {
                    Debug.Log("Taking Damage");
                    // Damage the player by the specified amount.
                    playerHealth.TakeDamage(damageAmount);
                }
            }
        }
    }
}
