using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<PlayerMovement>().TakeDamage(1, dir * 200f);
        }
    }
}
