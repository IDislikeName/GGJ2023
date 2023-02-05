using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public Image[] hearts;
    public Sprite full;
    public Sprite empty;
    // Start is called before the first frame update
    void Start()
    {
        hearts = new Image[5];
        for(int i = 0; i < 5; i++)
        {
            hearts[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth()
    {
        for (int i = 0; i < GameManager.Instance.health; i++)
        {
            hearts[i].sprite = full;
        }
        for (int i = GameManager.Instance.health; i < GameManager.Instance.MaxHealth; i++)
        {
            hearts[i].sprite = empty;
        }
    }
}
