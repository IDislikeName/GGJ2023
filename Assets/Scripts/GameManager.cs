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
    public int health = 5;
    public int MaxHealth = 5;
    public bool immune = false;
    [SerializeField]
    GameObject lostScreen;
    [SerializeField]
    GameObject wonScreen;
    public int[] Waves;
    public int waveNum;
    public int enemyQuota;
    public int enemiesLeft;

    public bool waiting;
    public Transform player;

    public Hearts hearts;

    [SerializeField]
    AudioClip scream;
    bool played;
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Playing;
        waveNum = 0;
        enemyQuota = Waves[waveNum];
        enemiesLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            currentState = GameState.Lost;
            if(!played)
                SoundManager.Instance.PlayEffect(scream);
            lostScreen.SetActive(true);
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<Rigidbody2D>().constraints= RigidbodyConstraints2D.FreezePosition;
            player.rotation = Quaternion.Euler(0,0,90);
        }
        if (currentState == GameState.Won)
        {
            wonScreen.SetActive(true);
            player.GetComponent<PlayerMovement>().canMove = false;
            immune = true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        if (enemiesLeft <= 0&&enemyQuota<=0)
        {
            if (waveNum < Waves.Length - 1&&!waiting)
            {
                waiting = true;
                waveNum++;
                StartCoroutine(wait());
                
                
            }
            if (waveNum == 3)
                currentState = GameState.Boss;
        }
    }
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        enemyQuota = Waves[waveNum];
        waiting = false;
    }
    public void UpdateHealth()
    {
        hearts.UpdateHealth();
    }
}
