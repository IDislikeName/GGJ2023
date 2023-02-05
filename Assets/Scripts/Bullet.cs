using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.6f);
        if(transform.parent.gameObject!=null)
            Destroy(transform.parent.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<EnemyHealth>().TakeDamage(1, 200f * dir);
            Destroy(gameObject);
        }
    }
}
