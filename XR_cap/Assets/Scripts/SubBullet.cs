using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBullet : MonoBehaviour
{
    public float F_Dmg;
    public int I_Per;

    public bool IsRotate;

    Rigidbody2D R_Rigid;

    private void Awake()
    {
        R_Rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Dead();
    }

    public void Init(float dmg, int per, Vector3 dir)
    {
        F_Dmg = dmg;
        I_Per = per;

        if (per >= 0)
        {
            switch (GameManager.Instance.LevelUp.items[1].Level)
            {
                default:
                    R_Rigid.velocity = dir * 100f;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || I_Per == -100)
            return;

        I_Per--;
        if (I_Per < 0)
        {
            R_Rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }


    void Dead()
    {
        Transform target = GameManager.Instance.Player.transform;
        Vector3 targetPos = target.position;
        float dir = Vector3.Distance(targetPos, transform.position);
        if (dir > 20f)
            this.gameObject.SetActive(false);
    }
}