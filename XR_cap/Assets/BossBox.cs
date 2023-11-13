using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // 그 뭐야 exp를 랜덤으로 획득 하는 그런 연출이 있었으면 좋겠음
            // 그 연출이 끝나면 

            // gameObject.SetActive(false);
        }
    }
}
