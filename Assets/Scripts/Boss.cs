using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    enum Attacks
    {
        Missiles,
        Tentacles,
        Summons,
    }
    Attacks currentAttack;
    [SerializeField]
    GameObject missile;
    [SerializeField]
    GameObject tentacle;
    [SerializeField]
    GameObject enemy;
    bool attacking;
    EnemyHealth eh;
    // Start is called before the first frame update
    void Start()
    {
        eh = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking&&eh.currentHp>0)
        {
            attacking = true;
            if (currentAttack == Attacks.Missiles)
            {
                StartCoroutine(Missiles());
            }
            else if (currentAttack == Attacks.Tentacles)
            {
                StartCoroutine(Tentacles());
            }
            else if(currentAttack == Attacks.Summons)
            {
                StartCoroutine(Summons());
            }

        }
        
    }
    public IEnumerator Missiles()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject m= Instantiate(missile);
            m.transform.position = transform.position;
        }
        yield return new WaitForSeconds(3f);
        attacking = false;
        currentAttack = Attacks.Tentacles;

    }
    public IEnumerator Tentacles()
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(2f);
            SpawnTentacles();
        }
        yield return new WaitForSeconds(3f);
        attacking = false;
        currentAttack = Attacks.Summons;

    }
    public IEnumerator Summons()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            SpawnEnemies();
            SpawnEnemies();
            SpawnEnemies();
        }
        yield return new WaitForSeconds(5f);
        attacking = false;
        currentAttack = Attacks.Missiles;

    }
    public void SpawnTentacles()
    {
        GameObject t = Instantiate(tentacle);
        t.transform.position = GameManager.Instance.player.position+new Vector3(0,0.8f,0);
    }
    public void SpawnEnemies()
    {
        GameObject e = Instantiate(enemy);
        e.transform.position = new Vector3(Random.Range(-38, -7), Random.Range(-8, -37));
    }
    
}
