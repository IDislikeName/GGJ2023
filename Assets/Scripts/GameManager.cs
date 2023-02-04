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

    enum GameState
    {
        Playing,
        paused,
        Won,
        Lost
    };
    [SerializeField]
    GameState currentState;
    public int health = 3;
    public bool immune = false;
    [SerializeField]
    GameObject lostScreen;
    [SerializeField]
    GameObject pauseScreen;


    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Playing;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            currentState = GameState.Lost;
            lostScreen.SetActive(true);
        }
    }
}
