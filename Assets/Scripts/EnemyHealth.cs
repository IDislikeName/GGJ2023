using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHp;
    public int currentHp;
    [HideInInspector]
    public SpriteRenderer sr;
    public bool dead;
    public bool shotgun = false;
    public GameObject shotGun;
    public bool boss;
    public GameObject carrot;

    public AudioClip carrotHit;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0&&!dead)
        {
            Die();
        }
    }
    public void TakeDamage(int damage, Vector2 knockback)
    {
        if (!boss)
        {
            if (GetComponent<EnemyCarrot>().ready)
            {
                SoundManager.Instance.PlayEffect(carrotHit);
                GetComponent<Animator>().SetTrigger("Hurt");
                currentHp -= damage;
                GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
                StartCoroutine(DamageCoroutine());
            }
        }
        else
        {
            SoundManager.Instance.PlayEffect(carrotHit);
            currentHp -= damage;
            GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
            StartCoroutine(DamageCoroutine());
        }
        
    }
    public IEnumerator DamageCoroutine()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }
    public void Die()
    {
        dead = true;
        if (shotgun)
        {
            GameObject s = Instantiate(shotGun);
            s.transform.position = transform.position;
        }

        if (!boss)
        {
            GameManager.Instance.enemiesLeft--;
            GetComponent<EnemyCarrot>().ready = false;
            GetComponent<Animator>().SetTrigger("Die");
        }
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        if (!boss)
        {
            if (Random.Range(0, 5) == 1)
            {
                GameObject c = Instantiate(carrot);
                c.transform.position = transform.position;
                c.GetComponent<SpriteRenderer>().flipX = sr.flipX;
            }
        }
        else
        {
            GameManager.Instance.currentState = GameManager.GameState.Won;
        }
        
        Destroy(gameObject);
    }
}
