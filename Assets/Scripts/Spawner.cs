using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField]
    float spawnTime = 5f;
    float currentCD = 0;
    [SerializeField]
    LayerMask obs;
    public GameObject shotgun;
    bool gunSpawned = false;
    bool bossSpawned = false;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            if (GameManager.Instance.waveNum == 0)
                spawnTime = 7f;
            if (GameManager.Instance.waveNum == 1)
            {
                spawnTime = 5f;
                if(GameManager.Instance.enemyQuota==1)
                if (!gunSpawned)
                {
                    GameManager.Instance.enemyQuota -= 1;
                    gunSpawned = true;
                    Instantiate(shotgun);

                }
            }
                
            if (GameManager.Instance.waveNum == 2)
            {
                spawnTime = 3f;
                
            }
               
            if (currentCD <= 0&&GameManager.Instance.enemyQuota>0)
            {
                GameManager.Instance.enemyQuota -= 1;
                currentCD = spawnTime;
                Spawn();
            }
            else
            {
                currentCD -= Time.deltaTime;
            }
        }
        else if(GameManager.Instance. currentState == GameManager.GameState.Boss&&!bossSpawned)
        {
            bossSpawned = true;
            boss.SetActive(true);
        }
    }
    public void Spawn()
    {
        float randx = Random.Range(-10,10);
        float randy = Random.Range(-10,10);
        Vector2 randv2 = new Vector2(randx, randy);
        Vector2 size = new Vector2(1, 1);
        Collider2D col =  Physics2D.OverlapBox(randv2+(Vector2)GameManager.Instance.player.position,size,0,obs);
        if (col!=null)
        {
            Debug.Log("failed");
            Spawn();
        }
        else
        {
            GameObject e = Instantiate(enemy);
            e.transform.position = randv2;
        }
        
    }
}
