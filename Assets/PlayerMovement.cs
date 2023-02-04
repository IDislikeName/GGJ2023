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
    float moveSpeed = 5f;
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

        if (horiz > 0)
        {
            sr.flipX = true;
        }
        else if (horiz < 0)
        {
            sr.flipX = false;
        }

        if (move == Vector2.zero)
            anim.SetBool("Walking", false);
        else
            anim.SetBool("Walking", true);
        
    }
    private void FixedUpdate()
    {
        rb.velocity = move * moveSpeed;
    }
}
