using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;


    [SerializeField] private TextMeshProUGUI timerText;
    public int recruitementTotalSeconds;
    public int getToCastleTotalSeconds;

    public List<Villager> recruitedVillagers;

    public enum GameState
    {
        Recruitement,
        GetToCastle,
        Attacking
    }

    public GameState gameState;

    public Transform castleAttackPoint;
    // Start is called before the first frame update

    private void Awake()
    {
        _instance = this;

        StartCoroutine(Timer());
    }

    void Start()
    {
        gameState = GameState.Recruitement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Timer()
    {
        int timerSecondsLeft = recruitementTotalSeconds;
        var ts = TimeSpan.FromSeconds(timerSecondsLeft);
        timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        //Recruitement period
        while (timerSecondsLeft > 0)
        {
            
            yield return new WaitForSeconds(1); //Else use regular scaled time that allows pauses

            timerSecondsLeft--;

            ts = TimeSpan.FromSeconds(timerSecondsLeft);
            timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }

        timerSecondsLeft = getToCastleTotalSeconds;
        gameState = GameState.GetToCastle;

        while (timerSecondsLeft > 0)
        {
            yield return new WaitForSeconds(1); //Else use regular scaled time that allows pauses

            timerSecondsLeft--;
        }

        Debug.Log("Lose condition");
    }

    public void BeginAttack()
    {
        gameState = GameState.Attacking;

        foreach (Villager villager in recruitedVillagers)
        {
            
        }
    }
}
