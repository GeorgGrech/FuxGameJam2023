using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Villager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] Transform characterCanvas;
    Slider recruitSlider;

    public bool recruited;
    public float recruitedLevel;

    [SerializeField] private Material recruitMaterial;

    private NavMeshAgent agent;

    public Transform player;

    [SerializeField] Collider detectEnemyTrigger;
    public List<Transform> enemiesInRange;

    private NPCFighter fighter;

    [SerializeField] private float startAttackDistance;

    // Start is called before the first frame update
    void Start()
    {
        fighter = GetComponent<NPCFighter>();

        recruitSlider = characterCanvas.Find("RecruitSlider").GetComponent<Slider>();

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player").transform;

        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        //villagerCanvas.forward = mainCam.transform.forward;
        characterCanvas.rotation = Camera.main.transform.rotation;

        if(recruited)
        {
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
            else if(gameManager.gameState != GameManager.GameState.Attacking)
            {
                agent.SetDestination(player.position);
                fighter.StopAttack();
            }
        }
    }

    public void EnableSlider(bool enabled)
    {
        recruitSlider.gameObject.SetActive(enabled);
    }

    public void UpdateSlider()
    {
        recruitSlider.value = recruitedLevel;
    }

    public void UpdateModel()
    {
        GetComponent<MeshRenderer>().material = recruitMaterial; //Will be replaced by change in the model
    }

    public void GoToAttackPoint(Vector3 attackPoint)
    {
        agent.SetDestination(attackPoint);
    }

    public void EnableDetectEnemy(bool enable)
    {
        if(detectEnemyTrigger) //Fixes an error, I think
            detectEnemyTrigger.enabled = enable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            enemiesInRange.Add(other.transform); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
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
}
