using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField]
    float spawnTime = 5f;
    float currentCD = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            if (currentCD <= 0&&GameManager.Instance.enemyQuota>0)
            {
                currentCD = spawnTime;
                Spawn();
            }
            else
            {
                currentCD -= Time.deltaTime;
            }
        }
        
    }
    public void Spawn()
    {
        float randx = Random.Range(-10,10);
        float randy = Random.Range(-10,10);
        Vector2 randv2 = new Vector2(randx, randy);
        Vector2 size = new Vector2(1, 1);
        Collider2D col =  Physics2D.OverlapBox(randv2,size,0);
        if (col!=null &&col.CompareTag("Obstacles"))
        {
            Spawn();
        }
        else
        {
            GameObject e = Instantiate(enemy);
            e.transform.position = randv2;
        }
        
    }
}
