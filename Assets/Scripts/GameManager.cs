using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public enum GameState
    {
        Playing,
        Boss,
        paused,
        Won,
        Lost
    };
    public GameState currentState;
    public int health = 3;
    public bool immune = false;
    [SerializeField]
    GameObject lostScreen;
    [SerializeField]
    GameObject pauseScreen;
    public int[] Waves;
    public int waveNum;
    public int enemyQuota;


    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Playing;
        waveNum = -1;
        enemyQuota = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            currentState = GameState.Lost;
            lostScreen.SetActive(true);
        }
        if (enemyQuota <= 0)
        {
            if (waveNum < Waves.Length - 1)
            {
                waveNum++;
                StartCoroutine(wait());
                
                
            }
            
        }
    }
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        enemyQuota = Waves[waveNum];
    }
}
