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

        if(recruited && gameManager.gameState!=GameManager.GameState.Attacking)
            agent.SetDestination(player.position);
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

}
