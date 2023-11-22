using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBox : MonoBehaviour
{
    SpriteRenderer SR;
    public Sprite[] Sprites;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (GameManager.Instance.LevelUp.IsLevelUp)
                StartCoroutine(OffBox());
        }
    }

    IEnumerator OffBox()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.LevelUp.Show();
            SR.sprite = Sprites[1];            
        }
    }
}
