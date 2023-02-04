using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchforkHitbox : MonoBehaviour
{
    public Pitchfork pf;
    // Start is called before the first frame update
    void Start()
    {
        pf = GetComponentInParent<Pitchfork>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<EnemyHealth>().TakeDamage(pf.damage, dir * pf.knockboack);
        }
    }
}
