using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    Animator anim;
    public bool ready = false;
    public GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Bruh());
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
            hitbox.SetActive(true);
        else
            hitbox.SetActive(false);
    }
    IEnumerator Bruh()
    {
        yield return new WaitForSeconds(1.5f);
        ready = true;
        anim.SetTrigger("Ready");
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Done");
        ready = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
