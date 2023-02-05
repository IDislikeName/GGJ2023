using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarrot : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 500f;
    [SerializeField]
    float maxSpeed = 4f;
    [SerializeField]
    float friction = 0.9f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    [SerializeField]
    int damage = 1;
    [SerializeField]
    float knockback = 3000;
    
    [SerializeField]
    float attackCD=2f;
    float currentCD=0;

    public Vector2 dir;

    public bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ready = false;
        StartCoroutine(Ready());
    }
    IEnumerator Ready()
    {
        yield return new WaitForSeconds(1f);
        ready = true;
    }
    // Update is called once per frame
    void Update()
    {
        currentCD -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (ready)
        {
            rb.AddForce(moveSpeed * dir * Time.deltaTime);

            
            if (dir.x > 0)
                sr.flipX = true;
            else if (dir.x < 0)
                sr.flipX = false;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            float spd = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, friction);
            rb.velocity = rb.velocity.normalized * spd;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentCD <= 0&&ready)
        {
            currentCD = attackCD;
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage,dir*knockback);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentCD<= 0&&ready)
        {
            currentCD = attackCD;
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage, dir * knockback);
        }
    }
}
