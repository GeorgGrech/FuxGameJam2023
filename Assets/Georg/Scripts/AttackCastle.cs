using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCastle : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) //OnTriggerStay just in case player happens to already be in trigger
    {
        if (other.CompareTag("Player") && gameManager.gameState==GameManager.GameState.GetToCastle && !gameManager.gameEnded)
        {
            gameManager.BeginAttack();
        }
    }
}
