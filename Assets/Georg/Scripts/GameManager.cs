using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float messageTime;
    private Coroutine messageCoroutine; //Store in a variable to avoid accidental override

    [SerializeField] private TextMeshProUGUI timerText;
    public int recruitementTotalSeconds;
    public int getToCastleTotalSeconds;

    public List<Villager> recruitedVillagers;

    public List<Transform> castleEnemies;
    public int castleEnemyCount;

    [SerializeField] private Transform door;
    [SerializeField] private PointerUI pointerUI;

    [SerializeField] private float endGameDelay;

    public bool gameEnded;

    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    private AudioSource audioSource;
    public enum GameState
    {
        Recruitement,
        GetToCastle,
        Attacking
    }

    public GameState gameState;

    // Start is called before the first frame update

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        gameState = GameState.Recruitement;
        messageText.text = string.Empty;

        pointerUI.Show(false);
        pointerUI.pointingTo = door.position;

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Timer()
    {
        int timerSecondsLeft = recruitementTotalSeconds;
        TimeSpan ts;

        DisplayMessage("Get the people riled up! Recruit them to your cause!");

        //Recruitement period
        while (timerSecondsLeft > 0)
        {
            
            ts = TimeSpan.FromSeconds(timerSecondsLeft);
            timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

            yield return new WaitForSeconds(1);

            timerSecondsLeft--;

        }

        timerSecondsLeft = getToCastleTotalSeconds;
        gameState = GameState.GetToCastle;

        DisplayMessage("It's time to tear your cruel overlords down! Head to the castle gates!");

        while (timerSecondsLeft > 0 && gameState!=GameState.Attacking)
        {
            pointerUI.Show(true);

            ts = TimeSpan.FromSeconds(timerSecondsLeft);
            timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

            yield return new WaitForSeconds(1);

            timerSecondsLeft--;

        }

        timerText.text = string.Empty;
        pointerUI.Show(false);

        if (gameState==GameState.Attacking)
        {
            DisplayMessage("ATTAAAAAAACK!!");
            door.GetComponent<Animator>().Play("DoorOpen");
        }
        else
        {
            GameEnded(false); //Lose
        }

    }

    public void BeginAttack()
    {
        gameState = GameState.Attacking;

        foreach (Villager villager in recruitedVillagers)
        {
            villager.EnableDetectEnemy(false); //Turn off detection trigger. Enemies will now be targeted even if out of range
            villager.enemiesInRange = castleEnemies; //Set possible enemies to attack as all castleEnemies
            //villager.GoToAttackPoint(castleAttackPoint.position);
        }
    }

    private void DisplayMessage(string message)
    {
        if(messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(DisplayMessageCoroutine(message));
    }

    private IEnumerator DisplayMessageCoroutine(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(messageTime);
        messageText.text = string.Empty;
    }

    public void CastleEnemyDeath(Transform enemy)
    {
        castleEnemies.Remove(enemy);
        castleEnemyCount--;

        if(castleEnemyCount <= 0)
        {
            GameEnded(true);
        }
    }

    public void GameEnded(bool win) //Add parameter later for type of loss?
    {
        if (!gameEnded) //Ensures no duplicate loss screens
        {
            gameEnded = true;
            StartCoroutine(DelayEndGame(win));
        }
    }

    private IEnumerator DelayEndGame(bool win)
    {
        yield return new WaitForSeconds(endGameDelay);

        if(win)
        {
            Debug.Log("Win condition");
            audioSource.clip = winSound;
            DisplayMessage("The monarchy has been overthrown! What a joyous day!");
        }
        else
        {
            Debug.Log("Lose Condition");
            audioSource.clip = loseSound;
            DisplayMessage("Welp...better luck next time?");
        }

        audioSource.Play();
    }
}
