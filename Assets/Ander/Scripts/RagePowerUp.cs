using UnityEngine;

public class RagePowerUp : MonoBehaviour
{
    [SerializeField] private float speedIncrease = 15.0f; // The amount to increase movement speed.
    [SerializeField] private int damageIncrease = 10;   // The amount to increase damage.
    [SerializeField] private float powerUpDuration = 10.0f; // Duration of the power-up effect in seconds.
    [SerializeField] private float rotationSpeed = 60f; // Degrees per second for rotation.

    private PlayerMove playerMove; // Reference to the PlayerMove script.
    private Fighter fighter;       // Reference to the Fighter script.

    private static bool powerUpActive = false; // Static flag to track if any power-up is already active.
    private static float powerUpEndTimeStatic = 0f; // Static time to track the end time of the active power-up.

    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        fighter = FindObjectOfType<Fighter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (you can use tags or layers for this).
        if (other.CompareTag("Player"))
        {
            // Check if a power-up is already active.
            if (!powerUpActive)
            {
                // Apply the speed and damage increases to the player.
                if (playerMove != null)
                {
                    playerMove.ApplySpeedIncrease(speedIncrease);
                }

                if (fighter != null)
                {
                    fighter.ApplyDamageIncrease(damageIncrease);
                }

                // Activate the power-up.
                powerUpActive = true;
                powerUpEndTimeStatic = Time.time + powerUpDuration;

                // Destroy the power-up object since it has been picked up.
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        // Rotate the object continuously around the Y-axis in a downward direction.
        transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);

        // Check if the power-up is active and has expired.
        if (powerUpActive && Time.time >= powerUpEndTimeStatic)
        {
            // Deactivate the power-up.
            DeactivatePowerUp();
        }
    }

    private void DeactivatePowerUp()
    {
        // Revert the speed and damage increases.
        if (playerMove != null)
        {
            playerMove.RevertSpeedIncrease();
        }

        if (fighter != null)
        {
            fighter.RevertDamageIncrease();
        }

        // Reset power-up status.
        powerUpActive = false;
    }
}
