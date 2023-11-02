using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        if(IsRotate)
            transform.Rotate(Vector3.forward * 0.5f);
    }

    public void Init(float dmg, int per, Vector3 dir)
    {
        F_Dmg = dmg;
        I_Per = per;

        if(per >= 0)
        {
            switch (GameManager.Instance.LevelUp.items[1].Level)
            {
                case 1:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 2:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 3:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 4:
                    R_Rigid.velocity = dir * 16f;
                    break;
                case 5:
                    R_Rigid.velocity = dir * 17f;
                    break;
                default:
                    R_Rigid.velocity = dir * 18f;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || I_Per == -100)
            return;

        I_Per--;

        if (GameManager.Instance.LevelUp.items[1].Level > 3)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject test = GameManager.Instance.P_Manager.Get(22);
                //test.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                test.transform.position = transform.position;
                //test.transform.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);

                Rigidbody2D rigid = test.GetComponent<Rigidbody2D>();
                SubBullet bulletlogic = test.GetComponent<SubBullet>();
                bulletlogic.F_Dmg = 1;
                if (GameManager.Instance.LevelUp.items[1].Level > 4)
                    bulletlogic.I_Per = 99;

                //bulletlogic.IsRotate = true;

                Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 10), Mathf.Sin(Mathf.PI * 2 * i / 10));

                if(GameManager.Instance.LevelUp.items[1].Level > 4)
                    rigid.AddForce(dirvec.normalized * 20, ForceMode2D.Impulse);
                else
                    rigid.AddForce(dirvec.normalized * 16, ForceMode2D.Impulse);

                Vector3 rotvec = Vector3.forward * 360 * i / 10 + Vector3.forward * 90;
                test.transform.Rotate(rotvec);
            }
        }

        if (I_Per < 0)
        {
            R_Rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
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
