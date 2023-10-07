using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    
    public GameObject HealthPowerUp; // The GameObject to drop.
    public GameObject RagePowerUp; // The GameObject to drop.


    public void DropHealth()
    {
        // Instantiate the itemToDrop GameObject at the enemy's position.
        Instantiate(HealthPowerUp, transform.position, Quaternion.identity);

    }


    public void DropRage()
    {
        // Instantiate the itemToDrop GameObject at the enemy's position.
        Instantiate(RagePowerUp, transform.position, Quaternion.identity);
    }
}