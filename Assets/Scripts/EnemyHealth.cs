using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    int maxHp;
    public int currentHp;
    [HideInInspector]
    public SpriteRenderer sr;
    public bool dead;
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
        currentHp -= damage;
        GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        StartCoroutine(DamageCoroutine());
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
        GameManager.Instance.enemyQuota -= 1;
        Destroy(gameObject);
    }
}
