using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // �� ���� exp�� �������� ȹ�� �ϴ� �׷� ������ �־����� ������
            // �� ������ ������ 

            // gameObject.SetActive(false);
        }
    }
}
