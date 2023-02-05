using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    bool chasing = false;
    float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.player.position, step);
        transform.up = ((Vector2)GameManager.Instance.player.position - (Vector2)transform.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<PlayerMovement>().TakeDamage(1,dir*200f);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Hitbox"))
        {
            Destroy(gameObject);
        }
    }
}
