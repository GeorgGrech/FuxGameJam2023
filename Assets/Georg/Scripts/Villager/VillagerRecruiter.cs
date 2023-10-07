using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VillagerRecruiter : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private List<Villager> unrecruitedInRange;

    [SerializeField] private float baseRecruitSpeed;

    //private SphereCollider radiusCollider;
    [SerializeField] private float recruitSizeIncrease = .15f;

    // Start is called before the first frame update
    void Start()
    {
        //radiusCollider = GetComponent<SphereCollider>();
        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState != GameManager.GameState.Attacking)
        {
            RecruitEnemyInput();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            Villager villager = other.GetComponent<Villager>();

            if (!villager.recruited)
            {
                unrecruitedInRange.Add(villager);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            Villager villager = other.GetComponent<Villager>();

            if (!villager.recruited)
            {
                unrecruitedInRange.Remove(villager);
            }
        }
    }

    void RecruitEnemyInput()
    {
        List<Villager> toRemove = new List<Villager>();
        if (Input.GetKey(KeyCode.E))
        {
            foreach (Villager villager in unrecruitedInRange)
            {
                villager.EnableSlider(true);
                villager.recruitedLevel += (baseRecruitSpeed * Time.deltaTime) / unrecruitedInRange.Count;
                villager.UpdateSlider();

                if (villager.recruitedLevel >= 1)
                {
                    RecruitVillager(villager);

                    toRemove.Add(villager);

                    IncreaseRadiusSize();
                }  
            }
            
            foreach (Villager villager in toRemove)
            {
                unrecruitedInRange.Remove(villager);
            }
        }
    }

    void RecruitVillager(Villager villager)
    {
        villager.recruited = true;
        //unrecruitedInRange.Remove(villager);
        villager.EnableSlider(false);

        villager.UpdateModel();
        gameManager.recruitedVillagers.Add(villager);

        villager.EnableDetectEnemy();
    }

    void IncreaseRadiusSize()
    {
        transform.localScale += new Vector3(recruitSizeIncrease, recruitSizeIncrease, recruitSizeIncrease);
    }
}
