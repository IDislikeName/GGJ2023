using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public SpriteRenderer sr;
    [HideInInspector]
    public Animator anim;
    Vector2 move;

    [SerializeField]
    Transform weapon;
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float maxSpeed = 5f;
    [SerializeField]
    float friction = 0.9f;

    public bool canMove = true;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        move = new Vector2(horiz, vert);
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > transform.position.x)
            sr.flipX = true;
        else if (mousePos.x < transform.position.x)
            sr.flipX = false;

        //point weapon
        weapon.transform.up = (mousePos - (Vector2)transform.position).normalized;
        if (weapon.rotation.eulerAngles.z > 180)
            weapon.GetComponentInChildren<SpriteRenderer>().flipX = false;
        else
            weapon.GetComponentInChildren<SpriteRenderer>().flipX = true;

        //walking animation
        if (move == Vector2.zero)
            anim.SetBool("Walking", false);
        else
            anim.SetBool("Walking", true);
        
    }
    private void FixedUpdate()
    {
        if (canMove&& move != Vector2.zero)
        {
            rb.AddForce(move * moveSpeed * Time.deltaTime);
            if (rb.velocity.magnitude > maxSpeed)
            {
                float spd = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, friction);
                rb.velocity = rb.velocity.normalized * spd;
            }
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, friction);
        }
    }
    public void TakeDamage()
    {
        if (!GameManager.Instance.immune)
        {
            GameManager.Instance.health -= 1;
            StartCoroutine(DamageCoroutine());
        }
    }
    public IEnumerator DamageCoroutine()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }
}