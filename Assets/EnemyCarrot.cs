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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 dir = (GameManager.Instance.player.transform.position - transform.position).normalized;
        rb.AddForce( moveSpeed*dir* Time.deltaTime);

        if (rb.velocity.magnitude > maxSpeed)
        {
            float spd = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, friction);
            rb.velocity = rb.velocity.normalized * spd;
        }
        if (rb.velocity.x > 0)
            sr.flipX = true;
        else if (rb.velocity.x < 0)
            sr.flipX = false;

    }
}
