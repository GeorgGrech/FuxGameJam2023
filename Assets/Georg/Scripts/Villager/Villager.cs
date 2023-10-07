using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Villager : MonoBehaviour
{
    GameManager gameManager;

    Camera mainCam;

    Transform villagerCanvas;
    Slider recruitSlider;

    public bool recruited;
    public float recruitedLevel;

    [SerializeField] private Material recruitMaterial;

    private NavMeshAgent agent;

    public Transform player;

    [SerializeField] Collider detectEnemyTrigger;
    [SerializeField] private List<Transform> enemiesInRange;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        villagerCanvas = transform.Find("Canvas");
        recruitSlider = villagerCanvas.Find("RecruitSlider").GetComponent<Slider>();

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player").transform;

        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        villagerCanvas.forward = mainCam.transform.forward;

        if(recruited && gameManager.gameState != GameManager.GameState.Attacking)
        {
            if (ClosestEnemy()) //If there is a closest enemy
            {
                agent.SetDestination(ClosestEnemy().position);
            }
            else
            {
                agent.SetDestination(player.position);
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

    public void EnableDetectEnemy()
    {
        detectEnemyTrigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }

    private Transform ClosestEnemy()
    {
        List<Transform> toRemove = new List<Transform>();


        Transform closestEnemy = null;
        float closestDistance = 10;
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
