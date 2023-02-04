using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitchfork : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    public bool attacking;
    [SerializeField]
    float attackCD = 0.5f;
    float currentCD = 0;

    public int damage = 1;
    public float knockboack = 200f;
    public GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !attacking && currentCD <= 0)
        {
            Attack();
            currentCD = attackCD;
        }
        else
        {
            currentCD -= Time.deltaTime;
            currentCD = Mathf.Max(0, currentCD);
        }
        if (attacking)
            hitbox.SetActive(true);
        else
            hitbox.SetActive(false);
    }
    public void Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }
}
