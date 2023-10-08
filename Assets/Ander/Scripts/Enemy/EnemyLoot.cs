using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public GameObject HealthPowerUp; // The GameObject for the health power-up.
    public GameObject RagePowerUp;   // The GameObject for the rage power-up.

    [Range(0f, 1f)]
    public float dropRate; // Drop rate for any power-up (0% to 100%).

    public void DropPowerUp()
    {
        // Check if a random value falls within the drop rate range.
        if (Random.value <= dropRate)
        {
            GameObject powerUpToDrop;

            if (HealthPowerUp == null)
                powerUpToDrop = RagePowerUp;

            else if (RagePowerUp == null)
                powerUpToDrop = HealthPowerUp;

            else
                powerUpToDrop = (Random.value <= 0.5f) ? HealthPowerUp : RagePowerUp;

            // Instantiate the chosen power-up at the enemy's position.
            Instantiate(powerUpToDrop, transform.position, Quaternion.identity);

            Debug.Log("Dropped: " + (powerUpToDrop == HealthPowerUp ? "Health" : "Rage"));
        }
    }
}
