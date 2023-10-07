using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private bool isCastleEnemy;

    private void Start()
    {
        if (isCastleEnemy)
        {
            GameManager._instance.castleEnemies.Add(transform);
            GameManager._instance.castleEnemyCount++;
        }
    }

    [ContextMenu("Kill Enemy")]
    public void Death()
    {
        if(isCastleEnemy)
        {
            GameManager._instance.CastleEnemyDeath(transform);
        }
        Destroy(gameObject);
    }
}
