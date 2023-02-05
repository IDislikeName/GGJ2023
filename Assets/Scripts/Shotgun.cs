using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    public bool attacking;
    [SerializeField]
    float attackCD = 0.8f;
    float currentCD = 0;

    public int damage = 1;
    public float knockboack = 300f;
    public GameObject bullets;

    public AudioClip shoot;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponentInParent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && !attacking && currentCD <= 0&&GameManager.Instance.health > 0)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Attack(mousePos);
            currentCD = attackCD;
        }
        else
        {
            currentCD -= Time.deltaTime;
            currentCD = Mathf.Max(0, currentCD);
        }
    }
    public void Attack(Vector2 mousePos)
    {
        SoundManager.Instance.PlayEffect(shoot);
        //GetComponent<Animator>().SetTrigger("Attack");
        GameObject b= Instantiate(bullets);

        b.transform.right = (mousePos - (Vector2)transform.position).normalized;
        b.transform.position = transform.position + b.transform.right * 0.6f;
    }
}
