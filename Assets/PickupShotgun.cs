using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShotgun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().unlockedGun = true;
            collision.GetComponent<PlayerMovement>().activeWeapon = collision.GetComponent<PlayerMovement>().gun;
            collision.GetComponent<PlayerMovement>().pitchfork.SetActive(false);
            collision.GetComponent<PlayerMovement>().gun.SetActive(true);
            Destroy(gameObject);
        }
    }
}
