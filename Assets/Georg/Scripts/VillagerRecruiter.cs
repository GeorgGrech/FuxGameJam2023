using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VillagerRecruiter : MonoBehaviour
{
    [SerializeField] private List<Villager> unrecruitedInRange;

    [SerializeField] private float baseRecruitSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger");
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

    void KeyboardInput()
    {
        List<Villager> toRemove = new List<Villager>();
        if (Input.GetKey(KeyCode.E))
        {
            foreach (Villager villager in unrecruitedInRange)
            {
                villager.recruitedLevel += baseRecruitSpeed * Time.deltaTime; //Modify to slow down with amount of villagers

                if (villager.recruitedLevel >= 1)
                {
                    villager.recruited = true;
                    //unrecruitedInRange.Remove(villager);
                    toRemove.Add(villager);
                }  
            }
            
            foreach (Villager villager in toRemove)
            {
                unrecruitedInRange.Remove(villager);
            }
        }
    }
}
