using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool isCastleEnemy;

    private NavMeshAgent agent;

    public List<Transform> enemiesInRange;

    GameManager gameManager;

    [SerializeField] Transform characterCanvas;

    [SerializeField] private float startAttackDistance;

    private NPCFighter fighter;
    private void Start()
    {
        fighter = GetComponent<NPCFighter>();
        agent = GetComponent<NavMeshAgent>();

        gameManager = GameManager._instance;
        if (isCastleEnemy)
        {
            gameManager.castleEnemies.Add(transform);
            gameManager.castleEnemyCount++;
        }
    }

    void Update()
    {
        characterCanvas.rotation = Camera.main.transform.rotation;

        if (ClosestEnemy()) //If there is a closest enemy
        {
            agent.SetDestination(ClosestEnemy().position);

            if (Vector3.Distance(transform.position, agent.destination) <= startAttackDistance)
            {
                fighter.StartAttack();
            }
            else
            {
                fighter.StopAttack();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemiesInRange.Add(other.transform);
        }
        else if (other.CompareTag("Villager") && !other.isTrigger)
        {
            if (other.GetComponent<Villager>().recruited) //Only attack recruited enemies
            {
                enemiesInRange.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")||(other.CompareTag("Villager") && !other.isTrigger))
        {
            enemiesInRange.Remove(other.transform);
        }
    }

    private Transform ClosestEnemy()
    {
        List<Transform> toRemove = new List<Transform>();


        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (Transform enemy in enemiesInRange)
        {
            if (!enemy)
            {
                toRemove.Add(enemy);
                continue;
            }

            float checkDistance = Vector3.Distance(enemy.position, transform.position);
            if (checkDistance < closestDistance)
            {
                closestDistance = checkDistance;
                closestEnemy = enemy;
            }
        }

        foreach (Transform enemy in toRemove)
        {
            enemiesInRange.Remove(enemy);
        }
        return closestEnemy;
    }

    [ContextMenu("Kill Enemy")]
    public void Death()
    {
        if(isCastleEnemy)
        {
            gameManager.CastleEnemyDeath(transform);
        }

        GetComponent<EnemyLoot>().DropPowerUp();
        
    }
}
