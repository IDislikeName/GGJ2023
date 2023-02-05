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

    public GameObject pitchfork;
    public GameObject gun;
    public bool unlockedGun = false;

    public GameObject activeWeapon;

    public AudioClip getHit;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        activeWeapon = pitchfork;
    }


    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        move = new Vector2(horiz, vert);
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (canMove)
        {
            if (mousePos.x > transform.position.x)
                sr.flipX = true;
            else if (mousePos.x < transform.position.x)
                sr.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Q)&&unlockedGun&&canMove)
        {
            if (activeWeapon == pitchfork)
            {
                activeWeapon = gun;
                pitchfork.SetActive(false);
                gun.SetActive(true);
            }
            else if (activeWeapon == gun)
            {
                activeWeapon = pitchfork;
                gun.SetActive(false);
                pitchfork.SetActive(true);
            }

        }


        if (canMove)
        {
            //point weapon
            if (activeWeapon == pitchfork)
            {
                if (!weapon.GetComponentInChildren<Pitchfork>().attacking)
                    weapon.transform.up = (mousePos - (Vector2)transform.position).normalized;
                if (weapon.rotation.eulerAngles.z > 180)
                    weapon.GetComponentInChildren<SpriteRenderer>().flipX = false;
                else
                    weapon.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else if (activeWeapon == gun)
            {
                if (!weapon.GetComponentInChildren<Shotgun>().attacking)
                    weapon.transform.up = (mousePos - (Vector2)transform.position).normalized;
                if (weapon.rotation.eulerAngles.z > 180)
                    weapon.transform.localScale = new Vector3(-1, 1, 1);
                else
                    weapon.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
        
        

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
            if ((activeWeapon == pitchfork&&weapon.GetComponentInChildren<Pitchfork>().attacking)|| (activeWeapon == gun&& weapon.GetComponentInChildren<Shotgun>().attacking))
            {
                rb.AddForce(move * moveSpeed * Time.deltaTime * 0.1f);
                if (rb.velocity.magnitude > maxSpeed)
                {
                    float spd = Mathf.Lerp(rb.velocity.magnitude, maxSpeed*0.1f, friction);
                    rb.velocity = rb.velocity.normalized * spd;
                }
            }
            else
            {
                rb.AddForce(move * moveSpeed * Time.deltaTime);
                if (rb.velocity.magnitude > maxSpeed)
                {
                    float spd = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, friction);
                    rb.velocity = rb.velocity.normalized * spd;
                }
            }
                
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, friction);
        }
    }
    public void TakeDamage(int damage,Vector2 knockback)
    {
        if (!GameManager.Instance.immune)
        {
            SoundManager.Instance.PlayEffect(getHit);
            GameManager.Instance.health -= damage;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            GameManager.Instance.UpdateHealth();
            StartCoroutine(DamageCoroutine());
        }
    }
    public IEnumerator DamageCoroutine()
    {
        sr.color = Color.red;
        GameManager.Instance.immune = true;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.immune = false;
    }
}
