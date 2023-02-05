using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            if(GameManager.Instance.health< GameManager.Instance.MaxHealth)
            {
                GameManager.Instance.health++;
                GameManager.Instance.UpdateHealth();
                Destroy(gameObject);
            }
        }
    }
}
